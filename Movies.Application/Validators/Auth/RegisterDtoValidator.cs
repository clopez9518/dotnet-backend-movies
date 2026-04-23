using FluentValidation;
using Movies.Application.DTOs.Auth;

namespace Movies.Application.Validators.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() { 
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("EMAIL_REQUIRED")
                .EmailAddress().WithMessage("EMAIL_INVALID");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("PASSWORD_REQUIRED");
            RuleFor(x => x.PasswordConfirm)
                .NotEmpty().WithMessage("PASSWORD_CONFIRM_REQUIRED")
                .Equal(x => x.Password).WithMessage("PASSWORDS_DO_NOT_MATCH");

        }
    }
}
