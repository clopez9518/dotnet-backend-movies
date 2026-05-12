

using Movies.Application.Interfaces;

namespace Movies.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task DeleteExpiredTokensAsync()
        {
            await _refreshTokenRepository.DeleteExpiredAsync();
        }
    }
}
