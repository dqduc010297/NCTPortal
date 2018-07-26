using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models
{
    public class WarehouseModel
    {
		public int Id { get; set; }

		public string WarehouseName { set; get; }
		public string PhoneNumber { get; set; }
		public string Address { set; get; }

		public string OwnerName { get; set; }
		public string OwnerPhone { get; set; }
	}

	public class WarehouseModelMapper : Profile
	{
		public WarehouseModelMapper()
		{
			var mapper = CreateMap<WarehouseModel, WareHouse>();

			var mappers = CreateMap<WareHouse, WarehouseModel>();

			mappers.ForMember(p => p.OwnerName, opt => opt.MapFrom(s => s.Owner.UserName));
			mappers.ForMember(p => p.OwnerPhone, opt => opt.MapFrom(s => s.PhoneNumber));
			mappers.ForMember(p => p.WarehouseName, opt => opt.MapFrom(s => s.NameWarehouse));
		}
	}
}
