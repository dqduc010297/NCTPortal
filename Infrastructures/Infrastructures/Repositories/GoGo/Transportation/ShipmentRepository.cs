using AutoMapper;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Repositories.Transportation;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Domains.GoGo.Models.Fleet_management;
using Domains.GoGo.Models;
using Domains.Core;

namespace Infrastructures.Repositories.GoGo.Transportation
{
	public class ShipmentRepository : GenericRepository<Shipment, int>, IShipmentRepository
	{
		private readonly IMapper _mapper;
		private readonly IShipmentRequestRepository _shipmentRequestRepository;

		public ShipmentRepository(IMapper mapper, IShipmentRequestRepository shipmentRequestRepository, ApplicationDbContext uoWContext) : base(uoWContext)
		{
			_mapper = mapper;
			_shipmentRequestRepository = shipmentRequestRepository;
		}

		public DataSourceResult GetAllAsync(DataSourceRequest request, string userId)
		{
			if (userId != null)
			{
				return this.dbSet.Where(p => (p.DriverId.ToString() == userId && p.Status!= ShipmentStatus.INACTIVE)).OrderByDescending(p=>p.CreatedDate).MapQueryTo<ShipmentModel>(_mapper).ToDataSourceResult(request);
			}

			return this.dbSet.MapQueryTo<ShipmentModel>(_mapper).ToDataSourceResult(request);
		}
  
        public async Task<ShipmentViewModel> GetShipmentAsync(string code)
        {
            int totalPackage = _shipmentRequestRepository.GetTotalRequest(code);
            string firstRequestCode = await _shipmentRequestRepository.GetFirstRequestCode(code);
            var query = this.dbSet
                        .Include(p => p.Vehicle)
                        .Where(p => p.Code == code)
                        .Select(p => new ShipmentViewModel
                        {
                            Id=p.Id,
                            Code = p.Code,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Status = p.Status,
                            RequestQuality = p.RequestQuantity,
                            LicensePlate = p.Vehicle.LicensePlate,
                            PackageQuality = totalPackage,
                            CurrentRequest=firstRequestCode
                        });
            return await query.FirstAsync();
        }
        public ShipmentDetailModel GetShipmentByCode(string Code)
        {
            return this.dbSet.Where(p => p.Code == Code).MapQueryTo<ShipmentDetailModel>(_mapper).SingleOrDefault();
        }
        public ShipmentDetailModel GetShipmentById(string id)
        {
            return this.dbSet.Where(p => p.Id.ToString() == id).MapQueryTo<ShipmentDetailModel>(_mapper).SingleOrDefault();
        }

        public async Task<int> ChangeStatus(string code, string status)
		{
			var shipment = await this.dbSet.Where(p => p.Code == code).FirstAsync();
			shipment.Status = status;
			this.context.Update(shipment);
			return await this.context.SaveChangesAsync();
		}

		public async Task<int> ChangeShipmentStatusById(string id, string status)
		{
			var shipment = await this.dbSet.Where(p => p.Id.ToString() == id).FirstAsync();
			shipment.Status = status;
			this.context.Update(shipment);
			return await this.context.SaveChangesAsync();
		}

		public Shipment GetShipment(string id)
		{
			return this.dbSet.Where(p => p.Id.ToString() == id).SingleOrDefault();
		}

		public async Task<string> ChangeDeliveryStatus(string code, string status)
		{
			var shipment = await this.dbSet.Where(p => p.Code == code).FirstAsync();
			shipment.Status = status;
			this.context.Update(shipment);
			await this.context.SaveChangesAsync();
			return shipment.Code;
		}
	}
}