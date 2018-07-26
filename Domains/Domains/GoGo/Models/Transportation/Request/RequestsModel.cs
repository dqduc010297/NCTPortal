using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class RequestsModel
    {
		public int Id { get; set; }

		//public DateTime CreatedDate { get; set; }
		public DateTime PickingDate { get; set; }
		public DateTime ExpectedDate { get; set; }

		public string DeliveryAddress { get; set; }

		public string WereHouseId { get; set; }
		public string WereHouseName { get; set; }
		public string WereHouseAddress { get; set; }

		public string CustomerName { get; set; }

		public int PackageQuantity { get; set; }

        public bool flag { get; set; } = true;

		public string Code { set; get; }
	}

	public class RequestModelMapper : Profile
	{
		public RequestModelMapper()
		{
			var mapper = CreateMap<RequestsModel, Request>();

			var mappers = CreateMap<Request, RequestsModel>();

			mappers.ForMember(p => p.WereHouseAddress, opt => opt.MapFrom(s => s.WareHouse.Address));
			mappers.ForMember(p => p.DeliveryAddress, opt => opt.MapFrom(s => s.Address));
			mappers.ForMember(p => p.CustomerName, opt => opt.MapFrom(s => s.Customer.UserName));
			mappers.ForMember(p => p.WereHouseName, opt => opt.MapFrom(s => s.WareHouse.NameWarehouse));
			mappers.ForMember(p => p.WereHouseId, opt => opt.MapFrom(s => s.WareHouse.Id));
		}
	}
}
