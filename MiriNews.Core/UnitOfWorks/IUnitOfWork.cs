using MiriNews.Core.Respositories;
using MiriNews.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
