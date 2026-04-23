


using Movies.Domain.Entities;

namespace Movies.Infrastructure.External.TMDb.Mappers
{
    public static class TmdbGenreMapper
    {
        public static List<MovieGenre> ToMovieGenres(List<int> genreIds, Dictionary<int, Genre> genreMap) {
            return genreIds
                .Where(id => genreMap.ContainsKey(id))
                .Select(id => new MovieGenre
                {
                    GenreId = genreMap[id].Id
                })
                .DistinctBy(mg => mg.GenreId)
                .ToList();
        }
    }
}
