using Groove.AspNetCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Groove.AspNetCore.UnitOfWork.EntityFramework
{
    public class UnitOfWork<TDbContext> : IUnitOfWork, IDisposable where TDbContext:DbContext
    {
        protected DbContext dbContext;
        protected IServiceProvider serviceProvider; 
        private bool disposed = false;


        public UnitOfWork(TDbContext context, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.dbContext = context;
        }

        public IUnitOfWorkTransactionScope BeginTransaction()
        {
            return new UnitOfWorkTransactionScope(this.dbContext).BeginTransaction();
        }


        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            var service = this.serviceProvider.GetService(typeof(TRepository));
            return service as TRepository;
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }


        public void Configure(UnitOfWorkOptions unitOfWorkOptions)
        {
            if (unitOfWorkOptions.Timeout.HasValue)
            {
                this.dbContext.Database.SetCommandTimeout(unitOfWorkOptions.Timeout.Value);
            }
        }


        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // TODO: review
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && dbContext != null)
                {
                    this.dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion Dispose
    }

}
