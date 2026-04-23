
using FluentValidation;
using Movies.Application.DTOs.Profile;

namespace Movies.Application.Validators.Profile
{
    public class AddToMyListDtoValidator : AbstractValidator<AddToMyListDto>
    {
        public AddToMyListDtoValidator() { 
            RuleFor(x => x.ProfileId)
                .GreaterThan(0).WithMessage("ProfileId must be greater than 0")
                .NotEmpty().WithMessage("ProfileId is required");
        }
    }
}
