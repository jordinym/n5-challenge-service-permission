using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using N5Permission.Domain.SeedWork;
using Nest;

namespace N5Permission.Application.Permission.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
    { 
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper; 
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionCommandHandler(IUnitOfWork unitOfWork, IElasticClient elasticClient, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        private IPermissionRepository _permissionRepository => _unitOfWork.Repository<IPermissionRepository>();

        public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionEntity = new PermissionEntity()
            {
                EmployeeForename = request.EmployeeForename,
                EmployeeSurname = request.EmployeeSurname,
                PermisionTypeId= (int)request.PermisionTypeId
            };
              _permissionRepository.Insert(permissionEntity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await SaveChangeLog(permissionEntity);
             
            return permissionEntity.Id;
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
