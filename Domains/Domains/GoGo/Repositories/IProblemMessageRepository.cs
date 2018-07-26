using Domains.GoGo.Entities;
using Domains.GoGo.Models;
using Groove.AspNetCore.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Repositories
{
    public interface IProblemMessageRepository : IGenericRepository<ProblemMessage, int>
    {
       Task<int> SaveProblemMessageAsync(string code, string message);
    }
}
