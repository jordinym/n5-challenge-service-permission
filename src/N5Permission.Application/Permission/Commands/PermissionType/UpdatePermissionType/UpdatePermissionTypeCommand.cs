using MediatR;

namespace N5Permission.Application.Permission.Commands.UpdatePermissionType
{
    public class UpdatePermissionTypeCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string? Description { get; set; } 
    }
}
