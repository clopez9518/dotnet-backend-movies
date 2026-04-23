

using FluentValidation;
using Movies.Application.DTOs.Genre;

namespace Movies.Application.Validators.Genre
{
    public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
    {
        public UpdateGenreDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(40).WithMessage("Name must not exceed 40 characters");
        }
    }
}
