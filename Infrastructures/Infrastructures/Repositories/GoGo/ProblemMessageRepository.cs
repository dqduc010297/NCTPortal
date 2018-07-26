using AutoMapper;
using Domains.GoGo.Entities;
using Domains.GoGo.Models;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Repositories;
using Domains.GoGo.Repositories.Transportation;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.GoGo.Transportation
{
    public class ProblemMessageRepository : GenericRepository<ProblemMessage, int>, IProblemMessageRepository
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _requestRepository;
        private readonly IShipmentRequestRepository _shipmentRequestRepository;
        public ProblemMessageRepository(IMapper mapper, IRequestRepository requestRepository, IShipmentRequestRepository shipmentRequestRepository, ApplicationDbContext dbContext) : base(dbContext)
        {
            _mapper = mapper;
            _requestRepository = requestRepository;
            _shipmentRequestRepository = shipmentRequestRepository;
        }

        public async Task<int> SaveProblemMessageAsync(string code, string message)
        {
            int RequestID = await _requestRepository.GetRequestID(code);
            var problem = new ProblemMessage()
            {
                IsSolve = false,
                Message = message,
                RequestId = RequestID
            };
            this.context.Add(problem);
            return this.context.SaveChanges();
        }
        //public async Task<string> ResolvedProblem(string code)
        //{
        //    //int RequestID = await _requestRepository.GetRequestID(code);
        //    //var request = this.dbSet.Where(p => p.RequestId == RequestID).FirstAsync();
        //    //var shipmentRqDbSet = this.context.Set<ShipmentRepository>();
        //    //var problemDbSet = this.context.Set<ProblemMessage>();
        //}

    }
}
