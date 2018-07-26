using AutoMapper;
using Domains.Core;
using Domains.GoGo.Models.Transportation;
using Domains.Identity.Entities;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities
{
    public class Request : IEntity<int>
    {
		public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        //Customer add 
        public DateTime PickingDate { get; set; }
        public DateTime ExpectedDate { set; get; }
       


		public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude{ get; set; }
        public string Address { set; get; }

        public int PackageQuantity { get; set; }

        public string Code { set; get; }
		public string Status { get; set; }

        public string ReceiverName { set; get; }
        public string ReceiverPhoneNumber { set; get; }
        
        public long IssuerId { get; set; }
		public int WareHouseId { get; set; }
		public long CustomerId { set; get; }

		public User Customer { set; get; }
		public User Issuer { get; set; }
		public WareHouse WareHouse { get; set; }
      
        //string IEntity<string>.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<Request, CustomerRequestModel>()
                .ForPath(destination => destination.WareHouse.Value, option => option.MapFrom(source => source.WareHouseId));
        }
    }
}
