using Groove.AspNetCore.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Groove.AspNetCore.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : class, IRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.
        /// </returns>
        int SaveChanges();

        Task<int> SaveChangesAsync();
        IUnitOfWorkTransactionScope BeginTransaction();

        /// <summary>
        /// Begin Unit Of Work
        /// </summary>
        /// <param name="unitOfWorkOptions"></param>
        void Configure(UnitOfWorkOptions unitOfWorkOptions);
    }

    public interface IUnitOfWork<TContext>
    {
        TContext Context { get; set; }
    }
}
