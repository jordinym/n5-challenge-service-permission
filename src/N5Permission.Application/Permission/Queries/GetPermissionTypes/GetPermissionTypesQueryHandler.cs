using AutoMapper;
using MediatR;
using N5Permission.Domain.AggregatesModel.PermissionAggregate;

namespace N5Permission.Application.Permission.Queries.GetPermissionTypes
{
    public class GetPermissionTypesQueryHandler : IRequestHandler<GetPermissionTypesQuery, List<GetPermissionTypesQueryResponse>>
    {
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IMapper _mapper;

        public GetPermissionTypesQueryHandler(IPermissionTypeRepository permissionTypeRepository, IMapper mapper)
        {
            _permissionTypeRepository = permissionTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<GetPermissionTypesQueryResponse>> Handle(GetPermissionTypesQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionTypeRepository.GetPermissionTypes();
            return _mapper.Map<List<PermissionTypeEntity>, List<GetPermissionTypesQueryResponse>>(permissions);
        }
    }
}
