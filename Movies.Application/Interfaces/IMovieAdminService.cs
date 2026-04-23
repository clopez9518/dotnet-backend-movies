using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;
using Movies.Application.DTOs.TMDb;

namespace Movies.Application.Interfaces
{
    public interface IMovieAdminService
    {
        Task<IEnumerable<MovieAdminDto>> GetAll();
        Task<MovieAdminDto> GetById(int id);
        Task<MovieAdminDto> Create(CreateMovieAdminDto createMovieDto);
        Task<MovieAdminDto> Update(int id, UpdateMovieAdminDto updateMovieDto);
        Task<MovieAdminDto> UpdateMovieGenres(int movieId, List<int> genreIds);
        Task<IEnumerable<TmdbMovieDto>> GetTmdbMoviesBySearch(string search);
        Task<TmdbMovieResponseDto> GetTmdbMovieById(int movieId);
    }
}
