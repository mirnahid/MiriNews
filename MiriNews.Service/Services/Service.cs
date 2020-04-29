using MiriNews.Core.Respositories;
using MiriNews.Core.Services;
using MiriNews.Core.UnitOfWorks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MiriNews.Service.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();            
        }

        public async Task AddRange(IQueryable<TEntity> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
        }

        public  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return  _repository.Find(predicate);
        }

        public  IQueryable<TEntity> GetAll()
        {
            return  _repository.GetAll();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            _unitOfWork.Commit();
        } 

        public void RemoveRange(IQueryable<TEntity> entities)
        {
            _repository.RemoveRange(entities);
            _unitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            _repository.Update(entity);
            _unitOfWork.Commit();
            return entity;
        }
    }
}
