

using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwt(User user, int? profileId = null);
        RefreshToken GenerateRefreshToken();
    }
}
