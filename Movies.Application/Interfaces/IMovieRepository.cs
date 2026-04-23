using Movies.Domain.Entities;


namespace Movies.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task AddAsync(Movie entity);
        Task<IEnumerable<Movie>> GetAllAsync(int page = 1, int pageSize = 10);
        Task<Movie?> GetByIdAsync(int id);
        Task LoadGenre(Movie movie);
        Task<IEnumerable<Movie>> GetAllAsyncAdmin();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int id);
        Task<IEnumerable<Movie>> GetTrendingMovies();
        Task<IEnumerable<Movie>> GetSimilarMovies(int movieId);
        Task<IEnumerable<Movie>> GetMoviesSortAsync(string sortBy = "recent", int limit = 15);
        void UpdateMovieGenres(Movie movie, List<int> genreIds);
        Task<int> TotalMoviesCountAsync();

        Task SaveChangesAsync();
    }
}
