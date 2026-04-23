

using Movies.Application.DTOs.Movie;

namespace Movies.Application.DTOs.Profile
{
    public class ProfileDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;

        public bool IsKids { get; set; }

        public int UserId { get; set; }
    }
}
