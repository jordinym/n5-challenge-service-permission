using FluentValidation;

namespace N5Permission.Application.Permission.Commands.UpdatePermission
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.EmployeeForename).NotEmpty();
            RuleFor(x => x.EmployeeSurname).NotEmpty();
            RuleFor(x => x.PermisionTypeId).NotEmpty();
        }
    }
}
