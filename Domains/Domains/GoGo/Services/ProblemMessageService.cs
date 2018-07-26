using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.GoGo.Models;
using Domains.GoGo.Repositories;
using Groove.AspNetCore.UnitOfWork;

namespace Domains.GoGo.Services
{
    public class ProblemMessageService : IProblemMessageService
    {
        private readonly IProblemMessageRepository _repository;
        private readonly IUnitOfWork _uow;

        public ProblemMessageService(IMapper mapper, IUnitOfWork uow, IProblemMessageRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }
        public async Task<int> SaveProblemMessageAsync(string code, string message)
        {
            return await _repository.SaveProblemMessageAsync(code, message);
        }
    }
}
