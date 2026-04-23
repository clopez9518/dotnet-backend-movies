

using Microsoft.EntityFrameworkCore;
using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.Persistence;

namespace Movies.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Include(u => u.Profiles)
                .ToListAsync();
        }

        public async Task<User?> GetByRefreshToken(string token)
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Include(u => u.Profiles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => 
                    u.RefreshTokens.Any(rt => rt.Token == token));
        }

       
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Include(u => u.Profiles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users
                .Where(u => u.IsActive)
                .Include(u => u.Profiles)
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAdmin()
        {
            return await _context.Users
                .Include(u => u.Profiles)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
