namespace Movies.Application.DTOs.TMDb
{
    public class TmdbCastDto
    {
        public bool Adult { get; set; }
        public int Gender { get; set; }
        public int Id { get; set; }
        public string Known_for_department { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Original_name { get; set; } = null!;
        public double Popularity { get; set; }
        public string Profile_path { get; set; } = null!;
        public int Cast_id { get; set; }
        public string Character { get; set; } = null!;
        public string Credit_id { get; set; } = null!;
        public int Order { get; set; }
    }
}
