using Domains.GoGo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domains.GoGo.Services
{
    public interface IProblemMessageService
    {
        Task<int> SaveProblemMessageAsync(string code, string message);
    }
}
