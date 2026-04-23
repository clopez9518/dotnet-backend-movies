using Movies.Application.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Movies.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {

        private AppDBContext _context;

        public GenreRepository(AppDBContext context) { 
            _context = context;
        
        }
        public async Task Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
        }

        public async Task Delete(int id)
        {
            _context.Remove(id);
        }

        public async Task<Genre?> Get(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Genre genre)
        {
            _context.Genres.Attach(genre);
            _context.Entry(genre).State = EntityState.Modified;
        }
    }
}
