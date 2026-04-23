

using Movies.Application.DTOs.MovieAdmin;
using Movies.Domain.Entities;

namespace Movies.Application.Extensions
{
    public static class MovieExtensions
    {

        public static void ApplyPatch(this Movie movie, UpdateMovieAdminDto dto)
        {
            movie.Title = dto.Title ?? movie.Title;
            movie.Description = dto.Description ?? movie.Description;
            movie.DurationMinutes = dto.DurationMinutes ?? movie.DurationMinutes;
            //movie.ThumbnailUrl = dto.ThumbnailUrl ?? movie.ThumbnailUrl;
            //movie.BackdropUrl = dto.BackdropUrl ?? movie.BackdropUrl;
            movie.VideoUrl = dto.VideoUrl ?? movie.VideoUrl;
            movie.ReleaseYear = dto.ReleaseYear ?? movie.ReleaseYear;
            movie.Rating = dto.Rating ?? movie.Rating;
            movie.IsTrending = dto.IsTrending ?? movie.IsTrending;
            movie.Cast = dto.Cast ?? movie.Cast;
            movie.Crew = dto.Crew ?? movie.Crew;


            if (dto.IsActive.HasValue)
            {
                if (dto.IsActive == false && movie.IsActive == true)
                {
                    movie.InactiveAt = DateTime.UtcNow;
                }
                else if (dto.IsActive == true && movie.IsActive == false)
                {
                    movie.InactiveAt = null;
                }

                movie.IsActive = dto.IsActive.Value;
            }
        }

        public static string BuildImageUrl(string path, int width) {
            const string baseUrl = "https://image.tmdb.org/t/p/";
            var size = $"w{width}";
            return $"{baseUrl}{size}{path}";
        } 

    }
}
