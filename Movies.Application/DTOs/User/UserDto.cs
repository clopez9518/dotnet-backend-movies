

using Movies.Application.DTOs.Profile;

namespace Movies.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;

        public DateTime? LastLoginAt { get; set; }

        public ICollection<ProfileDto> Profiles { get; set; } = new List<ProfileDto>();

        public DateTime CreatedAt { get; set; }
    }
}
