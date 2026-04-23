

using Movies.Domain.Entities;

namespace Movies.Application.Interfaces
{
    public interface IUserRepository
    {
        //Task<User> GetById(int id);
        Task Add(User user);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> GetAll();
        Task<IEnumerable<User>> GetUsersAdmin();
        Task<User?> GetByRefreshToken(string token);
        Task SaveChangesAsync();
    }
}
