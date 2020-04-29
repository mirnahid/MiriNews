using MiriNews.Core.Respositories;
using System;
using System.Threading.Tasks;

namespace MiriNews.Core.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        bool BeginNewTransaction();

        bool RollBackTransaction();

        Task<int> CommitAsync();

        int Commit();
    }
}
