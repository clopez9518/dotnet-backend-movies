namespace Movies.Application.DTOs.TMDb
{
    public class TmdbMovieDto
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; } = null!;
        public List<int>? Genre_ids { get; set; }
        public int Id { get; set; }
        public string Original_language { get; set; } = null!;
        public string Original_title { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public double Popularity { get; set; }
        public string Poster_path { get; set; } = null!;
        public string Release_date { get; set; } = null!;
        public string Title { get; set; } = null!;
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public int Vote_count { get; set; }

    }
}
