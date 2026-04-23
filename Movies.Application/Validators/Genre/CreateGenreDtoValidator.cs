

using FluentValidation;
using Movies.Application.DTOs.Genre;

namespace Movies.Application.Validators.Genre
{
    public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
    {
        public CreateGenreDtoValidator() {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Genre name cannot be empty.");         
        }
    }
}
