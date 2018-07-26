using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class FormShipmentModel
    {
		public int Id { get; set; }
		public string Code { get; set; }
		public List<int> RequestIdList { get; set; }

		public int RequestQuantity { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int VehicleId { get; set; }
		public long DriverId { get; set; }
		public long CoordinatorId { get; set; }
	}

	public class FormShipmentModelMapper : Profile
	{
		public FormShipmentModelMapper()
		{
			CreateMap<Shipment, FormShipmentModel>();
			var mappers = CreateMap<FormShipmentModel, Shipment>();

			mappers.ForMember(p => p.Id, opt => opt.MapFrom(s => s.Id));
			mappers.ForMember(p => p.Code, opt => opt.MapFrom(s => s.Code));
			mappers.ForMember(p => p.StartDate, opt => opt.MapFrom(s => s.StartDate));
			mappers.ForMember(p => p.EndDate, opt => opt.MapFrom(s => s.EndDate));
			mappers.ForMember(p => p.CoordinatorId, opt => opt.MapFrom(s => s.CoordinatorId));
			mappers.ForMember(p => p.RequestQuantity, opt => opt.MapFrom(s => s.RequestIdList.Count));
		}
	}

	
}
