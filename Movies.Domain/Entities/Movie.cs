

namespace Movies.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DurationMinutes { get; set; }

        public string ThumbnailUrl { get; set; } = null!;
        public string BackdropUrl { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public double Rating { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsTrending { get; set; } = false;
        public DateTime? InactiveAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<Profile> Profiles { get; set; } = new List<Profile>();
        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public List<string> Cast { get; set; } = new List<string>();
        public List<string> Crew { get; set; } = new List<string>();
    }
}
