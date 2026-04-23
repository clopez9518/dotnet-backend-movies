

namespace Movies.Application.DTOs.TMDb
{
    public class TmdbMovieResponseDto
    {
        public string? Backdrop_path { get; set; }
        public List<TmdbGenreDto>? Genres { get; set; }
        public int Id { get; set; }
        public string? Overview { get; set; }
        public string? Poster_path { get; set; }
        public string? Release_date { get; set; }
        public int Runtime { get; set; }
        public double Popularity { get; set; }
        public string? Title { get; set; }
        public double Vote_average { get; set; }
        public TmdbCreditsDto? Credits { get; set; }
    }
}
