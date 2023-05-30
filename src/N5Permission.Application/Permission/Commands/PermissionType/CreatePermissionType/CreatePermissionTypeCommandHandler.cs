using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using Nest;

namespace N5Permission.Application.Permission.Commands.CreatePermissionType
{
    public class CreatePermissionTypeCommandHandler : IRequestHandler<CreatePermissionTypeCommand, int>
    {
        private readonly IPermissionTypeRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public CreatePermissionTypeCommandHandler(IPermissionTypeRepository permissionRepository, IElasticClient elasticClient, IMapper mapper)
        { 
            _permissionRepository = permissionRepository;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            var permissionTypeEntity = new PermissionTypeEntity()
            {
                Description= request.Description
            };
            _permissionRepository.Add(permissionTypeEntity);
           

            await _permissionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            //await SaveChangeLog(permissionTypeEntity);
             
            return permissionTypeEntity.Id;
        }

        private Task<IndexResponse> SaveChangeLog(PermissionEntity permissionEntity)
        {
            var permissionChangeLog = _mapper.Map<PermissionEntity, PermissionChangeLog>(permissionEntity);
            permissionChangeLog.NewEntity = true;
            permissionChangeLog.SourceContext = "N5Permission.Application.Permission.Commands.CreatePermission.CreatePermissionCommandHandler";
            return _elasticClient.IndexDocumentAsync(permissionChangeLog);
        }
    }
}
