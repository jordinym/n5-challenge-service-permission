using MediatR;

namespace N5Permission.Application.Permission.Commands.CreatePermissionType
{
    public class CreatePermissionTypeCommand : IRequest<int>
    {
        public string? Description { get; set; } 
    }
}
