

using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface IProfileRepository
    {
        Task<Profile?> GetById(int id);
        Task<IEnumerable<Profile>> GetAllUserProfiles(int userId);
        Task Add(Profile profile);
        Task<Profile?> GetByIdAndUserId(int profileId, int userId);

        Task<IEnumerable<Movie>> GetMyList(int profileId);
        Task<IEnumerable<int>> GetMyListMoviesIds(int profileId);
        Task AddToMyList(Profile profile, int movieId);
        Task RemoveFromMyList(int profileId, int movieId);
        Task<bool> ExistsInMyList(int profileId, int movieId); 
        Task Delete(int profileId);

        Task SaveChangesAsync();
    }
}
