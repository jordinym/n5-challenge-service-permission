using FluentValidation;

namespace N5Permission.Application.Permission.Commands.UpdatePermissionType
{
    public class UpdatePermissionTypeCommandValidator : AbstractValidator<UpdatePermissionTypeCommand>
    {
        public UpdatePermissionTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Description).NotEmpty(); 
        }
    }
}
