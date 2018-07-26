using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Groove.AspNetCore.UnitOfWork
{
    public interface IGenericRepository<TEntity, TKey>: IRepository<TEntity>
        where TEntity : class
    {

        void Create(TEntity entity);

        void Delete(TKey id);

        void Delete(TEntity entityToDelete);

        TEntity GetEntityById(TKey id);

        Task<TEntity> GetEntityByIdAsync(TKey id);
        void Update(TEntity entityToUpdate);
    }
}
