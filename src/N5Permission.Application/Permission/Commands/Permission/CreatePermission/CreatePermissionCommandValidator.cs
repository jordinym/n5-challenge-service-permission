using FluentValidation;

namespace N5Permission.Application.Permission.Commands.CreatePermission
{
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(x => x.EmployeeForename).NotEmpty();
            RuleFor(x => x.EmployeeSurname).NotEmpty();
            RuleFor(x => x.PermisionTypeId).NotEmpty();
        }
    }
}
