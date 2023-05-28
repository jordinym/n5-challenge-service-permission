using MediatR;

namespace N5Permission.Application.Permission.Commands.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string? EmployeeForename { get; set; }
        public string? EmployeeSurname { get; set; }
        public int? PermisionTypeId { get; set; }
    }
}
