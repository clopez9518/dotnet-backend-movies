

using Movies.Application.DTOs.User;

namespace Movies.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set;} = null!;
        public DateTime ExpiresAt { get; set; }
        public UserDto User { get; set; }

    }
}
