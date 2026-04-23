

using FluentValidation;
using Movies.Application.DTOs.Profile;

namespace Movies.Application.Validators.Profile
{
    public class CreateProfileDtoValidator : AbstractValidator<CreateProfileDto>
    {
        public CreateProfileDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("NAME_REQUIRED")
                .MaximumLength(20).WithMessage("NAME_TOO_LONG");
            RuleFor(x => x.IsKids)
                .NotNull().WithMessage("IS_KIDS_REQUIRED");
        }
    }
}
