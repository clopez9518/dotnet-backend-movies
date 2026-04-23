using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Movies.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private AppDBContext _context;

        public MovieRepository(AppDBContext context)
        {
            _context = context;
        }

        private IQueryable<Movie> GetBaseMoviesQuery()
        {
            return _context.Movies
                .Where(m => m.IsActive)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre);
        }

        //Public Methods
        public async Task<IEnumerable<Movie>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            var movies = await GetBaseMoviesQuery()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(g => g.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
       
        public async Task LoadGenre(Movie movie)
        {
            await _context.Entry(movie)
                .Reference(m => m.MovieGenres)
                .LoadAsync();
        }

        public async Task<IEnumerable<Movie>> GetTrendingMovies()
        {
            return await GetBaseMoviesQuery()
                .Where(m => m.IsTrending)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int id)
        {
            return await GetBaseMoviesQuery()
                .Where(m => m.MovieGenres.Any(mg => mg.GenreId == id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetSimilarMovies(int movieId)
        {
            var currentMovie = await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (currentMovie == null) return Enumerable.Empty<Movie>();

            var currentGenres = currentMovie.MovieGenres.Select(mg => mg.GenreId).ToList();
            var currentCast = currentMovie.Cast;
            var currentCrew = currentMovie.Crew;

            var movies = await GetBaseMoviesQuery()
                .Where(m => m.Id != movieId &&
                            m.MovieGenres.Any(mg => currentGenres.Contains(mg.GenreId)))
                .ToListAsync();

            var result = movies.Select(m => new
            {
                Movie = m,
                Score = m.MovieGenres.Count(mg => currentGenres.Contains(mg.GenreId)) * 3 +
                (m.Cast != null ? m.Cast.Intersect(currentCast).Count() * 2 : 0) +
                (m.Crew != null ? m.Crew.Intersect(currentCrew).Count() * 2 : 0)
            })
            .OrderByDescending(x => x.Score)
            .ThenByDescending(x => x.Movie.Rating)
            .Take(10)
            .Select(x => x.Movie);

            return result;
        }

        public async Task<int> TotalMoviesCountAsync()
        {
            return await _context.Movies.CountAsync(m => m.IsActive);
        }

        //Admin Methods

        public async Task<IEnumerable<Movie>> GetAllAsyncAdmin()
        {
            var movies = await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(g => g.Genre)
                .ToListAsync();
            return movies;
        }

        public void Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
        }

        public async Task AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
        }

        public void Update(Movie movie)
        {
            _context.Movies.Attach(movie);
            _context.Entry(movie).State = EntityState.Modified;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateMovieGenres(Movie movie, List<int> genreIds)
        {
            movie.MovieGenres.Clear();

            movie.MovieGenres = genreIds
                .Select(genreId => new MovieGenre
                    {
                        MovieId = movie.Id,
                        GenreId = genreId
                    })
                .ToList();
        }

        public async Task<IEnumerable<Movie>> GetMoviesSortAsync(string sortBy = "recent", int limit = 15)
        {
            var query = GetBaseMoviesQuery()
                .AsQueryable();

            query = sortBy.ToLower() switch
            {
                "top-rated" => query.OrderByDescending(m => m.Rating),
                "release" => query.OrderByDescending(m => m.ReleaseYear),
                _ => query.OrderByDescending(m => m.CreatedAt)
            };

            return await query.Take(limit).ToListAsync();
        }

        
    }
}
