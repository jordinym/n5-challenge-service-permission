using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using Nest;

namespace N5Permission.Application.Permission.Commands.UpdatePermissionType
{
    public class UpdatePermissionTypeCommandHandler : IRequestHandler<UpdatePermissionTypeCommand, int>
    {
        private readonly IPermissionTypeRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public UpdatePermissionTypeCommandHandler(IPermissionTypeRepository permissionRepository, IElasticClient elasticClient, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            var permissionTypeEntity = await _permissionRepository.GetPermissionById((int)request.Id);
            if (permissionTypeEntity == null)
            {
                throw new Exception("permission type not exists");
            }

            permissionTypeEntity.Description = request.Description; 

            _permissionRepository.Update(permissionTypeEntity);
            await _permissionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
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
