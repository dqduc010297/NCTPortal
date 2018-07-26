using AutoMapper;
using Domains.Core;
using Domains.GoGo;
using Domains.GoGo.Models;
using Domains.GoGo.Repositories;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using System.Threading.Tasks;
using Domains.GoGo.Entities;

namespace Infrastructures.Repositories.GoGo
{
    public class WarehouseRepository : GenericRepository<WareHouse, int>, IWarehouseRepository
	{
		private readonly IMapper _mapper;

		public WarehouseRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
		{
			_mapper = mapper;
		}

		public async Task<WarehouseModel> GetWarehouseDetailAsync(int id)
		{
			return await this.dbSet.Include(p => p.Owner)
									.Where(p => p.Owner.Id == p.OwnerId && p.Id == id)
									.MapQueryTo<WarehouseModel>(_mapper).FirstAsync();
		}

		public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value)
		{

			return await this.dbSet.Where(p => ((p.Address.Contains(value)) || (p.NameWarehouse.Contains(value))))										
													.Select(p => new DataSourceValue<int>
													{
														DisplayName = $"{p.NameWarehouse}",
														Value = p.Id
													}).ToListAsync();
		}

        public WarehouseModel GetWarehouseByIdlAsync(string shipmentId)
        {
            var requestId = this.context.Set<ShipmentRequest>().Where(p => ((p.ShipmentId.ToString() == shipmentId) &&(p.Status != ShipmentRequestStatus.INACTIVE)) ).Select(p => p.RequestId).FirstOrDefault();
            return this.context.Set<Request>().Where(p => p.Id == requestId).Select(p => p.WareHouse).MapQueryTo<WarehouseModel>(_mapper).SingleOrDefault();
        }

        // Đ
        public async Task<IEnumerable<DataSourceValue<int>>> GetOnFilter(string displayName, long userId)
        {
            return await this.dbSet.Where(p => p.Address.Contains(displayName) && p.OwnerId == userId)
                .MapQueryTo<DataSourceValue<int>>(_mapper).ToListAsync(); 
        }
        // End Đ
    }
}

