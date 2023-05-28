using AutoMapper;
using MediatR;
using N5Permission.Application.Permission.Queries.GetPermissions;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;

namespace N5Permission.Application.Permission.Queries.GetPermissionWithPagination
{
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<GetPermissionsQueryResponse>>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }
        public async Task<List<GetPermissionsQueryResponse>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetPermissions(); 
            return _mapper.Map<List<PermissionEntity>, List<GetPermissionsQueryResponse>>(permissions);
        }
    }
}
