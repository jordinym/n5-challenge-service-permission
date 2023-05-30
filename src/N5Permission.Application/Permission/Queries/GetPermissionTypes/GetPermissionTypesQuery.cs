using MediatR; 

namespace N5Permission.Application.Permission.Queries.GetPermissionTypes
{
    public class GetPermissionTypesQuery : IRequest<List<GetPermissionTypesQueryResponse>>
    {
    }
}
