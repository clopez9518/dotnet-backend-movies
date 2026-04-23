

namespace Movies.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public int IdTmdb { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }
}
