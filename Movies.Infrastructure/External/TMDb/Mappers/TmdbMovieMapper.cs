

using Movies.Domain.Entities;
using Movies.Application.DTOs.TMDb;

namespace Movies.Infrastructure.External.TMDb.Mappers
{
    public static class TmdbMovieMapper
    {
        public static Movie ToMovie(this TmdbMovieResponseDto dto, List<Genre> genres)
        {
            return new Movie
            {
                Title = dto.Title,
                Description = dto.Overview,
                ReleaseYear = DateTime.Parse(dto.Release_date).Year,
                Rating = dto.Vote_average,
                ThumbnailUrl = dto.Poster_path,
                BackdropUrl = dto.Backdrop_path ?? dto.Poster_path,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsTrending = dto.Popularity >= 80, 
                DurationMinutes = dto.Runtime,
                VideoUrl = $"https://www.themoviedb.org/movie/{dto.Id}",
                Cast = dto.Credits.Cast?.Take(5).Select(c => c.Name).ToList() ?? new List<string>(),
                Crew = dto.Credits.Crew?.Take(5).Select(c => c.Name).ToList() ?? new List<string>(),
                MovieGenres = genres.Where(g => dto.Genres.Any(dg => dg.Id == g.IdTmdb))
                                  .Select(g => new MovieGenre { GenreId = g.Id })
                                  .ToList()
            };
        }
    }
}
