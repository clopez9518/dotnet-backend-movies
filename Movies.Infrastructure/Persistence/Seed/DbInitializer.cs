using Microsoft.EntityFrameworkCore;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.External.TMDb.Mappers;
using Movies.Infrastructure.External.TMDb.Services;

namespace Movies.Infrastructure.Persistence.Seed
{
    public class DbInitializer
    {
        private readonly ITmdbService _tmdbService;
        private readonly AppDBContext _context;

        public DbInitializer(ITmdbService tmdbService, AppDBContext context)
        {
            _tmdbService = tmdbService;
            _context = context;
        }

        public async Task SeedAsync()
        {
            //_context.Users.RemoveRange(_context.Users);
            //_context.SaveChanges();

            //_context.Database.ExecuteSqlRaw(@"
            //    TRUNCATE TABLE ""MovieGenres"", ""Movies"", ""Genres""
            //    RESTART IDENTITY CASCADE;
            //");

            if (!_context.Genres.Any())
            {
                var tmdbGenres = await _tmdbService.GetGenres();

                var genres = tmdbGenres.Select(g => new Genre
                {
                    IdTmdb = g.Id,
                    Name = g.Name
                }).ToList();

                _context.Genres.AddRange(genres);
                await _context.SaveChangesAsync();
            }

            if (!_context.Users.Any())
            {
                var user = new User
                {
                    Email = "admin@movies.com",
                    PasswordHash = "hashed_password",
                    CreatedAt = DateTime.UtcNow,
                    Role = "admin"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var profile = new Profile
                {
                    Name = "Admin",
                    UserId = user.Id,
                    IsKids = false,
                    AvatarUrl = "https://picsum.photos/200/200?random=1"
                };

                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
            }

            if (!_context.Movies.Any())
            {
                var genres = await _context.Genres.ToListAsync();

                var now = DateTime.UtcNow;

                var movies = new List<Movie>();
                var moviesFromTmdb = await _tmdbService.GetPopularMoviesAsync(2);

                foreach (var tmdbMovie in moviesFromTmdb)
                {

                    var movie = await _tmdbService.GetMovieDetailsAsync(tmdbMovie.Id);

                    if (movie == null) continue;

                    var movieMap = TmdbMovieMapper.ToMovie(movie, genres);
                    movies.Add(movieMap);
                }

                _context.Movies.AddRange(movies);
                await _context.SaveChangesAsync();     
            }
        }
    }
}
