

using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Profile;
using Movies.Application.DTOs.User;
using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> CreateDefault(UserDto user);
        Task<ProfileDto> Add(CreateProfileDto dto, int userId);
        Task<ProfileDto> GetById(int profileId);
        Task<IEnumerable<ProfileDto>> GetAllUserProfiles(int userId);
        Task<IEnumerable<MovieDto>> GetMyList(int profileId, int userId);
        Task<IEnumerable<int>> GetMyListMoviesIds(int profileId);
        Task<MovieDto> AddToMyList(int profileId, int movieId, int userId);
        Task<MovieDto> RemoveFromMyList(int profileId, int movieId, int userId);
        Task<bool> UserOwnsProfile(int userId, int profileId);
        Task Delete(int profileId, int userId);
    }
}
