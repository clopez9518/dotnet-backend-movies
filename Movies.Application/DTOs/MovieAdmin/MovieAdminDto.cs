using Movies.Application.DTOs.Genre;

namespace Movies.Application.DTOs.MovieAdmin
{
    public class MovieAdminDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string BackdropUrl { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public double Rating { get; set; }
        public bool IsActive { get; set; }
        public bool IsTrending { get; set; }
        public DateTime? InactiveAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<GenreDto> Genres { get; set; } = new();
        public List<string> Cast { get; set; } = new();
        public List<string> Crew { get; set; } = new();

    }
}
