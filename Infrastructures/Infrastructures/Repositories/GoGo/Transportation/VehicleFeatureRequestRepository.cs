using AutoMapper;
using Domains.GoGo.Entities;
using Domains.GoGo.Entities.Fleet;
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
    public class VehicleFeatureRequestRepository : GenericRepository<VehicleFeatureRequest, int>, IVehicleFeatureRequestRepository
    {
        private readonly IMapper _mapper;

        public VehicleFeatureRequestRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
        {
            _mapper = mapper;
        }

        public VehicleFeatureRequest FindVehicleFeatureAsync(int requestId)
        {
            var result = this.dbSet.Include(p => p.VehicleFeature).Where(p => p.RequestId == requestId).FirstOrDefault();
            return result;
        }

        public async Task<VehicleFeatureRequest> GetByRequestIdAsync(int requestId)
        {
            return await this.dbSet.Where(p => p.RequestId == requestId).FirstOrDefaultAsync(); ;
        }

        //public void SaveChangesAsync(VehicleFeatureRequest entity)
        //{
        //    this.context.Add(entity);
        //    this.context.SaveChangesAsync();
        //}
    }
}
