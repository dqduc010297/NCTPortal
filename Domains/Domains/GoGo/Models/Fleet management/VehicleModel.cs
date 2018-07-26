using AutoMapper;
using Domains.GoGo.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Fleet_management
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleFeatureType { get; set; }
    }

    public class VehicleModelMapper : Profile
    {
        public VehicleModelMapper()
        {
            CreateMap<VehicleModel, Vehicle>();
            var mappers = CreateMap<Vehicle, VehicleModel>();


            mappers.ForMember(p => p.VehicleFeatureType, opt => opt.MapFrom(s => s.VehicleType.TypeName));

        }
    }
}
