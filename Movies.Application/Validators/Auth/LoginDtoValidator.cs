

using FluentValidation;
using Movies.Application.DTOs.Auth;

namespace Movies.Application.Validators.Auth
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator() { 
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("EMAIL_REQUIRED")
                .EmailAddress().WithMessage("EMAIL_INVALID");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("PASSWORD_REQUIRED");
               
        }
    }
}
