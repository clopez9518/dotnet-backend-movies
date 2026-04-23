

using Movies.Application.DTOs.Movie;

namespace Movies.Application.DTOs.Profile
{
    public class ProfileDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string AvatarUrl { get; set; } = null!;

        public bool IsKids { get; set; }

        public List<MovieDto> MyList { get; set; } = new List<MovieDto>();

        public int UserId { get; set; }
    }
}
