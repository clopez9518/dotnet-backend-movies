

using Movies.Application.DTOs.Movie;
using Movies.Application.DTOs.Profile;
using Movies.Application.DTOs.User;
using Movies.Application.DTOs.UserAdmin;

namespace Movies.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(CreateUserDto createUserDto);
        Task<UserDto> GetUserByEmail(string email);
        Task<IEnumerable<UserAdminDto>> GetUsers();
        Task<UserAdminDto> ChangeUserStatusAdmin(int userId, bool isActive);
        Task<UserDto> GetUserById(int userId);
    }
}
