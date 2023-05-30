using MediatR;

namespace N5Permission.Application.Permission.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<int>
    {
        public string? EmployeeForename { get; set; }
        public string? EmployeeSurname { get; set; }
        public int? PermisionTypeId { get; set; }
    }
}
