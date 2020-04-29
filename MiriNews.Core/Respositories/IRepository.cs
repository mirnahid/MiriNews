using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiriNews.Core.Respositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity>  Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IQueryable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IQueryable<TEntity> entities);

        TEntity Update(TEntity entity);

    }
}
