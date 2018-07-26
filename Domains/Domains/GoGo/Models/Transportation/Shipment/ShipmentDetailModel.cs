using AutoMapper;
using Domains.GoGo.Entities;
using Domains.GoGo.Models.Fleet_management;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
	public class ShipmentDetailModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public IEnumerable<RequestsModel> RequestList { get; set; }
		public IEnumerable<int> RequestIdList { get; set; }
		public int RequestQuantity { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public WarehouseModel Warehouse { get; set; }
		public VehicleModel Vehicle { get; set; }
		public DriverModel Driver { get; set; }
	}

	public class ShipmentDetailModelMapper : Profile
	{
		public ShipmentDetailModelMapper()
		{
			var mappers =CreateMap<Shipment, ShipmentDetailModel>();
			CreateMap<ShipmentDetailModel, Shipment>();

			mappers.ForMember(p => p.Vehicle, opt => opt.MapFrom(s => s.Vehicle));
			mappers.ForMember(p => p.Driver, opt => opt.MapFrom(s => s.Driver));
		}
	}
}
