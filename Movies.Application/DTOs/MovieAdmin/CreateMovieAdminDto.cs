namespace Movies.Application.DTOs.MovieAdmin
{
    public class CreateMovieAdminDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public double Rating { get; set; }
        public string ThumbnailUrl { get; set; } = null!;
        public string BackdropUrl { get; set; } = null!;
        public string VideoUrl { get; set; } = null!;
        public List<string> Cast { get; set; } = new List<string>();
        public List<string> Crew { get; set; } = new List<string>();
    }
}
