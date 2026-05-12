

namespace Movies.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task DeleteExpiredTokensAsync();
    }
}
