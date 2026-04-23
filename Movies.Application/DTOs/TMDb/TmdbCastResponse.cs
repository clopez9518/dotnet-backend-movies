namespace Movies.Application.DTOs.TMDb
{
    public class TmdbCastResponse
    {
        public int Id { get; set; }
        public List<TmdbCastDto> Cast { get; set; } = new List<TmdbCastDto>();
        public List<TmdbCrewDto> Crew { get; set; } = new List<TmdbCrewDto>();
    }
}
