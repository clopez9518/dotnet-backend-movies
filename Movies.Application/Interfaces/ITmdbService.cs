

using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.DTOs.TMDb;
using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface ITmdbService
    {
        Task<IEnumerable<TmdbMovieDto>> GetPopularMoviesAsync(int page);
        Task<TmdbMovieResponseDto> GetMovieDetailsAsync(int movieId);
        Task<IEnumerable<TmdbMovieDto>> GetMoviesBySearch(string search);
        Task<IEnumerable<TmdbGenreDto>> GetGenres();
    }
}
