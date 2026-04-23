

using FluentValidation;
using Movies.Application.DTOs.MovieAdmin;

namespace Movies.Application.Validators.MovieAdmin
{
    public class CreateMovieAdminDtoValidator : AbstractValidator<CreateMovieAdminDto>
    {
        public CreateMovieAdminDtoValidator() {             
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");
            RuleFor(x => x.ReleaseYear)
                .NotEmpty().WithMessage("Release year is required.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("Release year cannot be in the future.");
            RuleFor(x => x.DurationMinutes)
                .NotEmpty().WithMessage("Duration is required.");
            RuleFor(x => x.ThumbnailUrl)
                .NotEmpty().WithMessage("Thumbnail URL is required.");
            RuleFor(x => x.BackdropUrl)
                .NotEmpty().WithMessage("Backdrop URL is required.");
        }
    }
}
