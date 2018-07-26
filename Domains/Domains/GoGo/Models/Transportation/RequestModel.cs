using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.Identity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class WaitingRequestModel
    {
        public int Id { get; set; }

        //public DateTime CreatedDate { get; set; }
        public DateTime PickingDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }

        public int PackageQuantity { get; set; }

        public string Code { set; get; }
        public string Status { get; set; }
    }

    public class WaitingRequestModelMaper : Profile
    {
        public WaitingRequestModelMaper()
        {
            CreateMap<WaitingRequestModel, Request>();
        }
    }
    public class CustomerRequestModel
    {
        public int Id { get; set; }
        
        //Customer add 
        public DateTime PickingDate { get; set; }
        public DateTime ExpectedDate { set; get; }
        //public string WarehouseName { set; get; }


        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }
        public string Address { set; get; }

        public int PackageQuantity { get; set; }

        public string Code { set; get; }
        public string Status { get; set; }

        public string ReceiverName { set; get; }
        public string ReceiverPhoneNumber { set; get; }
        
        //public int WareHouseId { get; set; }
        //public int VehicleFeatureId { get; set; }
        public DataSourceValue<int> WareHouse { get; set; }
        public DataSourceValue<int> VehicleFeature { get; set; }

    }

    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<CustomerRequestModel, Request>()
                .ForPath(destination => destination.WareHouseId, option => option.MapFrom(source => source.WareHouse.Value));
            CreateMap<Request, CustomerRequestModel>()
                .ForPath(destination => destination.WareHouse.Value, option => option.MapFrom(source => source.WareHouseId));
        }
    }

    public class CustomerRequestModelValidator : AbstractValidator<CustomerRequestModel>
    {
        public CustomerRequestModelValidator()
        {
            RuleFor(p => p.ExpectedDate).NotEmpty();
            RuleFor(p => p.PickingDate).NotEmpty();
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.PackageQuantity).NotEmpty();
            RuleFor(p => p.ReceiverName).NotEmpty();
            RuleFor(p => p.ReceiverPhoneNumber).NotEmpty();                             

        }
    }

    public class SummaryRequestModel
    {
        public int Id { get; set; }
        public string Code { set; get; }
        public string Status { get; set; }
        public DateTime PickingDate { get; set; }
        public DateTime ExpectedDate { set; get; }
        public string WareHouse { get; set; }
        public string Address { set; get; }
    }
}
