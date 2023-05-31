using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using Nest;

namespace N5Permission.Application.Permission.Commands.CreatePermissionType
{
    public class CreatePermissionTypeCommandHandler : IRequestHandler<CreatePermissionTypeCommand, int>
    { 
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionTypeCommandHandler(IUnitOfWork unitOfWork, IElasticClient elasticClient, IMapper mapper)
        {  
            _elasticClient = elasticClient;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        } 

       private IPermissionTypeRepository _permissionRepository => _unitOfWork.Repository<IPermissionTypeRepository>();

        public async Task<int> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            var permissionTypeEntity = new PermissionTypeEntity()
            {
                Description= request.Description
            };
            _permissionRepository.Insert(permissionTypeEntity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
