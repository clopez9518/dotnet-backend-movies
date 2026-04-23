

using Movies.Domain.Entities;
using System.Collections;

namespace Movies.Application.Interfaces
{
    public interface IGenreRepository
    {
        Task Add(Genre genre);
        void Update(Genre genre);

        Task Delete(int id);
        Task<Genre?> Get(int id);
        Task<IEnumerable<Genre?>> GetAll();

        Task SaveChangesAsync();

    }
}
