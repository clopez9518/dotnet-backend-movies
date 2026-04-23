using FluentValidation;
using Movies.Application.DTOs.UserAdmin;


namespace Movies.Application.Validators.UserAdmin
{
    public class ChangeUserStatusAdminDtoValidator : AbstractValidator<ChangeUserStatusAdminDto>
    {
        public ChangeUserStatusAdminDtoValidator() { 
                RuleFor(x => x.isActive)
                    .NotNull()
                    .WithMessage("User status is required.");
        }
    }
}
