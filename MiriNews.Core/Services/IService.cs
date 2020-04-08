using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiriNews.Core.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        IQueryable<TEntity> GetAllAsync();

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task AddRange(IQueryable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IQueryable<TEntity> entities);

        TEntity Update(TEntity entity);
    }
}
