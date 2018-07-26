using AutoMapper;
using Domains.GoGo.Entities;
using Domains.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Fleet_management
{
    public class DriverModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class DriverModelMapper : Profile
    {
        public DriverModelMapper()
        {
            var mappers = CreateMap<User, DriverModel>();
            CreateMap<DriverModel, User>();

            mappers.ForMember(p => p.Id, opt => opt.MapFrom(s => s.Id));
            mappers.ForMember(p => p.UserName, opt => opt.MapFrom(s => s.UserName));
            mappers.ForMember(p => p.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber));
        }
    }
}
