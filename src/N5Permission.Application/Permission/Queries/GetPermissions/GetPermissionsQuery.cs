using MediatR;
using N5Permission.Application.Permission.Queries.GetPermissions;

namespace N5Permission.Application.Permission.Queries.GetPermissionWithPagination
{
    public class GetPermissionsQuery: IRequest<List<GetPermissionsQueryResponse>>
    {
    }
}
