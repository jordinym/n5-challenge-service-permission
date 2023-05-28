using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Dto;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;
using Nest;

namespace N5Permission.Application.Permission.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, int>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;

        public UpdatePermissionCommandHandler(IPermissionRepository permissionRepository, IElasticClient elasticClient, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _elasticClient = elasticClient;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionEntity = await _permissionRepository.GetPermissionById((int)request.Id);
            if (permissionEntity == null)
            {
                throw new Exception("permission not exists");
            }

            permissionEntity.EmployeeForename = request.EmployeeForename;
            permissionEntity.EmployeeSurname = request.EmployeeSurname;
            permissionEntity.PermisionTypeId = (int)request.PermisionTypeId;

            _permissionRepository.Update(permissionEntity);
            await _permissionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
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
