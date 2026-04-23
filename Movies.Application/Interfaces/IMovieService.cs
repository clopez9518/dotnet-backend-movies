
using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.MovieAdmin;


namespace Movies.Application.Interfaces
{
    public interface IMovieService
    {
        Task<(IEnumerable<MovieDto> Movies, int TotalRecords, int PageSize)> GetAll(int page = 1, int pageSize = 10, int? profileId = null);
        Task<MovieDto> GetById(int id, int? profileId);
        Task<IEnumerable<MovieDto>> GetByGenre(int genreId, int? profileId);
        Task<IEnumerable<MovieDto>> GetTrendingMovies(int? profileId);
        Task<IEnumerable<MovieDto>> GetSimilarMovies(int movieId, int? profileId);
        Task<IEnumerable<MovieDto>> GetSortMovies(string sortBy = "recent", int limit = 15, int? profileId = null);
        Task<MovieDto> GetHeroMovie(int? genreId, int? profileId);
    }
}
