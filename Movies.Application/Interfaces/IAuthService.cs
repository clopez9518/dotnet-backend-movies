

using Movies.Application.DTOs.Auth;

namespace Movies.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> Login(LoginDto dto);
        Task<AuthResponseDto?> Refresh(string token);
        Task<AuthResponseDto?> Register(RegisterDto dto);
        Task<AuthResponseDto?> SelectProfile(int userId, int profileId);

        Task Logout(string refreshToken);
    }
}
