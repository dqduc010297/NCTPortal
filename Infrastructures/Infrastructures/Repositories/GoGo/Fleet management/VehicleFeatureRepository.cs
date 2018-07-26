using Domains.GoGo.Entities.Fleet;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Domains.GoGo.Services.Fleet_management;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Groove.AspNetCore.UnitOfWork;
using Domains.Core;
using System.Threading.Tasks;
using System.Linq;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using Microsoft.EntityFrameworkCore;
using Infrastructures;

namespace Domains.GoGo.Repositories.Fleet_management
{
    public class VehicleFeatureRepository : GenericRepository<VehicleFeature, int>, IVehicleFeatureRepository
    {
        private readonly IMapper _mapper;

        public VehicleFeatureRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DataSourceValue<int>>> GetOnFilter(string displayName)
        {
            return await this.dbSet.Where(p => p.FeatureName.Contains(displayName)).MapQueryTo<DataSourceValue<int>>(_mapper).ToListAsync();
        }
    }
}
