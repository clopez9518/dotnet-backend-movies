using FluentValidation;
using Movies.Application.DTOs.MovieAdmin;

namespace Movies.Application.Validators.MovieAdmin
{
    public class UpdateMovieGenresDtoValidator : AbstractValidator<UpdateMovieGenresDto>
    {
        public UpdateMovieGenresDtoValidator()
        {
            RuleFor(x => x.GenreIds)
                .NotNull()
                .WithMessage("GenreIds cannot be null.")
                .Must(ids => ids.Count > 0)
                .WithMessage("At least one genre ID must be provided.");
        }
    }
}
