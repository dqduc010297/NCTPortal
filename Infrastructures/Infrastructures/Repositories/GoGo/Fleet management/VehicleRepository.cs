using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Repositories.Fleet_management;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.GoGo.Fleet_management
{
    public class VehicleRepository : GenericRepository<Vehicle, int>, IVehicleRepository
    {
        private readonly IMapper _mapper;

        public VehicleRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
        {
            _mapper = mapper;
        }

        public async Task<VehicleModel> GetVehicleDetailAsync(int id)
        {
            return await this.dbSet.Include(p => p.VehicleType)
                                    .Where(p => p.VehicleType.Id == p.VehicleTypeId && p.Id == id)
                                    .MapQueryTo<VehicleModel>(_mapper).FirstAsync();
        }

        public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value)
        {
			var vehicleIdList = this.context.Set<Shipment>().Select(p => p.VehicleId).ToList();

			return await this.dbSet.Where(p => ((p.LicensePlate.Contains(value) || (p.VehicleType.TypeName.Contains(value))) && !vehicleIdList.Contains(p.Id))
													&& !vehicleIdList.Contains(p.Id))
													.Select(p => new DataSourceValue<int>
													{
														DisplayName = $"{p.LicensePlate}",
														Value = p.Id
													}).ToListAsync();
        }

    }
}
