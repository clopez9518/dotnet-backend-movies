

namespace Movies.Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task DeleteExpiredAsync();
    }
}
