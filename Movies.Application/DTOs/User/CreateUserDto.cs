

namespace Movies.Application.DTOs.User
{
    public class CreateUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordConfirm { get; set; } = null!;
        public string Role { get; set; } = "user";
    }
}
