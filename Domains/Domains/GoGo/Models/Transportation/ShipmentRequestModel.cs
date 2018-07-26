using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    class ShipmentRequestModel
    {
		public int Id { get; set; }
		public int RequestOrder { get; set; }
		public int ShipmentId { get; set; }
		public int RequestId { get; set; }
	}

	public class ShipmentRequestModelMapper : Profile
	{
		public ShipmentRequestModelMapper()
		{
			CreateMap<ShipmentRequest, ShipmentRequestModel>();
			CreateMap<ShipmentRequestModel, ShipmentRequest>();
		}
	}
}
