using Microsoft.EntityFrameworkCore.Storage;
using MiriNews.Core.Respositories;
using MiriNews.Core.UnitOfWorks;
using MiriNews.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace MiriNews.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MiriContext _context;

        private IDbContextTransaction _dbContextTransaction;

        private bool disposed;

        public UnitOfWork(MiriContext context)
        {
            _context = context;
        }
        public bool BeginNewTransaction()
        {
            try
            {
                _dbContextTransaction = _context.Database.BeginTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Commit()
        {
            var transaction = _dbContextTransaction != null ? _dbContextTransaction : _context.Database.BeginTransaction();

            using (transaction)
            {
                try
                {
                    if (_context == null)
                    {
                        throw new ArgumentException("Context is null");
                    }
                    int result = _context.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback();
                    throw new ArgumentException("Error on savechanges", ex);
                }
            }
        }

        public async Task<int> CommitAsync()
        {
            var transaction = _dbContextTransaction != null ? _dbContextTransaction : await _context.Database.BeginTransactionAsync();

            using (transaction)
            {
                try
                {
                    if (_context == null)
                    {
                        throw new ArgumentException("Context is null");
                    }
                    int result = await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ArgumentException("Error on savechanges", ex);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool RollBackTransaction()
        {
            try
            {
                _dbContextTransaction.Rollback();
                _dbContextTransaction = null;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }
    }
}
