

using Movies.Application.Interfaces;
using Movies.Infrastructure.Persistence;

namespace Movies.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private AppDBContext _context;

        public RefreshTokenRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task DeleteExpiredAsync()
        {
            var tokens = _context.RefreshTokens
            .Where(x => x.Expires < DateTime.UtcNow);

            _context.RefreshTokens.RemoveRange(tokens);
            await _context.SaveChangesAsync();
        }
    }
}
