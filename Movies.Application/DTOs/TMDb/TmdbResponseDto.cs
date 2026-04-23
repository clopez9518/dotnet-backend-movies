namespace Movies.Application.DTOs.TMDb
{
    public class TmdbResponseDto
    {
        public int Page { get; set; }
        public List<TmdbMovieDto>? Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }
}
