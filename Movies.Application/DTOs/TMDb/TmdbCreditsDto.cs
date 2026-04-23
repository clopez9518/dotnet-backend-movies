
namespace Movies.Application.DTOs.TMDb
{
    public class TmdbCreditsDto
    {
        public List<TmdbCastDto>? Cast { get; set; }
        public List<TmdbCrewDto>? Crew { get; set; }
    }
}
