using AutoMapper;
using MediatR;
using N5Permission.Application.Common.Exceptions;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using Nest;

namespace N5Permission.Application.Permission.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, int>
    { 
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork, IElasticClient elasticClient, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }

        private IPermissionRepository _permissionRepository => _unitOfWork.Repository<IPermissionRepository>();

        public async Task<int> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionEntity = await _permissionRepository.FirstOrDefaultAsync(r => r.Id == (int)request.Id);
            if (permissionEntity == null)
            {
                throw new NotFoundException(nameof(PermissionEntity), (int)request.Id);
            }

            permissionEntity.EmployeeForename = request.EmployeeForename;
            permissionEntity.EmployeeSurname = request.EmployeeSurname;
            permissionEntity.PermisionTypeId = (int)request.PermisionTypeId;

            _permissionRepository.Update(permissionEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await SaveChangeLog(permissionEntity);

            return permissionEntity.Id;
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
