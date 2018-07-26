using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
	public class ShipmentModel
	{
		public string Code { set; get; }
		public int Id { set; get; }
		public int RequestQuantity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { set; get; }
        public string VehicleLicensePlate { get; set; }
        public string DriverName { get; set; }
    }

    public class ShipmentModelMapper : Profile
    {
        public ShipmentModelMapper()
        {
            CreateMap<ShipmentModel, Shipment>();

            var mappers = CreateMap<Shipment, ShipmentModel>();

			mappers.ForMember(p => p.DriverName, opt => opt.MapFrom(s => s.Driver.UserName));
			mappers.ForMember(p => p.Code, opt => opt.MapFrom(s => s.Code));
			mappers.ForMember(p => p.VehicleLicensePlate, opt => opt.MapFrom(s => s.Vehicle.LicensePlate));
		}
	}
}
