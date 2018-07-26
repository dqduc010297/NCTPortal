using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.UnitOfWork
{
    public interface IUnitOfWorkTransactionScope : IDisposable
    {
        IUnitOfWorkTransactionScope BeginTransaction();
        void Commit();
        void Rollback();
    }
}
