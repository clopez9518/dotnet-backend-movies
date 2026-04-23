using System.Linq.Expressions;

namespace Movies.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task SaveChangesAsync();

        //void Delete(TEntity entity);
        //Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        //IQueryable<TEntity> Query();

    }
}
