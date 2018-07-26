using AutoMapper;
using Groove.AspNetCore.Domain.Entities;
using Groove.AspNetCore.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Groove.AspNetCore.UnitOfWork.EntityFramework
{
    public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected DbContext context;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(DbContext dbContext)
        {
            this.context = dbContext;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(TKey id)
        {
            TEntity entityToDelete = GetEntityById(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual TEntity GetEntityById(TKey id)
        {
            return dbSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public virtual Task<TEntity> GetEntityByIdAsync(TKey id)
        {
            return dbSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
