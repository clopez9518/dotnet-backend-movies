

using Microsoft.EntityFrameworkCore;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.Persistence;

namespace Movies.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private AppDBContext _context;
        public ProfileRepository(AppDBContext context) 
        { 
            _context = context; 
        }

        public async Task Add(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public async Task AddToMyList(Profile profile, int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie != null)
            {
                profile.MyList.Add(movie);
            }
        }

        public async Task Delete(int profileId)
        {
            await _context.Profiles
                .Where(p => p.Id == profileId).ExecuteDeleteAsync();
        }

        public async Task<bool> ExistsInMyList(int profileId, int movieId)
        {
            return await _context.Profiles
                .AsNoTracking()
                .Where(p => p.Id == profileId)
                .SelectMany(p => p.MyList)
                .AnyAsync(m => m.Id == movieId);
        }

        public async Task<IEnumerable<Profile>> GetAllUserProfiles(int userId)
        {
            return await _context.Profiles
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Profile?> GetById(int id)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Profile?> GetByIdAndUserId(int profileId, int userId)
        {
            return await _context.Profiles
                .FirstOrDefaultAsync(p => p.Id == profileId && p.UserId == userId);
        }

        public async Task<IEnumerable<Movie>> GetMyList(int profileId)
        {
            return await _context.Profiles
                .Where(p => p.Id == profileId)
                .SelectMany(p => p.MyList)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> GetMyListMoviesIds(int profileId)
        {
            return await _context.Profiles
                .Where(p => p.Id == profileId)
                .SelectMany(p => p.MyList)
                .Select(m => m.Id)
                .ToListAsync();
        }

        public async Task RemoveFromMyList(int profileId, int movieId)
        {
            var profile = await _context.Profiles
                .Include(p => p.MyList.Where(m => m.Id == movieId))
                .FirstOrDefaultAsync(p => p.Id == profileId);

            if (profile != null && profile.MyList.Any())
            {
                var movieToRemove = profile.MyList.First();
                profile.MyList.Remove(movieToRemove);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
