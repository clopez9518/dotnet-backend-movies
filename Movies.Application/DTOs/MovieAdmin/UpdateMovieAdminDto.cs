

namespace Movies.Application.DTOs.MovieAdmin
{
    public class UpdateMovieAdminDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int? ReleaseYear { get; set; }
        public int? DurationMinutes { get; set; }
        public string? VideoUrl { get; set; }

        public double? Rating { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTrending { get; set; }
        public DateTime? InactiveAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<string>? Cast { get; set; }
        public List<string>? Crew { get; set; } 
    }
}