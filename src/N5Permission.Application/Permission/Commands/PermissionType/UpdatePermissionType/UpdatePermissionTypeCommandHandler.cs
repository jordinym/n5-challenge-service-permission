using AutoMapper;
using MediatR;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using Nest;

namespace N5Permission.Application.Permission.Commands.UpdatePermissionType
{
    public class UpdatePermissionTypeCommandHandler : IRequestHandler<UpdatePermissionTypeCommand, int>
    { 
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionTypeCommandHandler(IUnitOfWork unitOfWork, IElasticClient elasticClient, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        private IPermissionTypeRepository _permissionRepository => _unitOfWork.Repository<IPermissionTypeRepository>();

        public async Task<int> Handle(UpdatePermissionTypeCommand request, CancellationToken cancellationToken)
        { 
            var permissionTypeEntity = await _permissionRepository.FirstOrDefaultAsync(r => r.Id == (int)request.Id);
            if (permissionTypeEntity == null)
            {
                throw new NotFoundException(nameof(PermissionTypeEntity), (int)request.Id);
            }

            permissionTypeEntity.Description = request.Description; 

            _permissionRepository.Update(permissionTypeEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            //await SaveChangeLog(permissionTypeEntity);

            return permissionTypeEntity.Id;
        }

        private Task<IndexResponse> SaveChangeLog(PermissionEntity permissionEntity)
        {
            var permissionChangeLog = _mapper.Map<PermissionEntity, PermissionChangeLog>(permissionEntity);
            permissionChangeLog.ModifiedEntity = true;
            permissionChangeLog.SourceContext = "N5Permission.Application.Permission.Commands.UpdatePermission.UpdatePermissionCommandHandler";
            return _elasticClient.IndexDocumentAsync(permissionChangeLog);
        }
    }
}
