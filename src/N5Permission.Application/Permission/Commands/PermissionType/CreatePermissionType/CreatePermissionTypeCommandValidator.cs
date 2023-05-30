using FluentValidation;

namespace N5Permission.Application.Permission.Commands.CreatePermissionType
{
    public class CreatePermissionTypeCommandValidator : AbstractValidator<CreatePermissionTypeCommand>
    {
        public CreatePermissionTypeCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty(); 
        }
    }
}
