using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class RequestDetailModel
    {
        public int PackageQuantity { get; set; }
        public string Code { set; get; }
        public string ReceiverName { set; get; }
        public string ReceiverPhoneNumber { set; get; }
        public DateTime EstimateDate { set; get; }
        public string Status { set; get; }
        public LocationModel Location { set; get; }
        public int RequestOrder { set; get; }
        public bool IsProblem { set; get; }
    }
    public class RequestDetaiModelMapper : Profile
    {
        public RequestDetaiModelMapper()
        {
            CreateMap<RequestDetailModel, Request>();
        }
    }
}
