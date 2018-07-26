using AutoMapper;
using Domains.Core;
using Groove.AspNetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Entities.Fleet
{
    public class VehicleFeature : IEntity<int>
    {
		public int Id { get; set; }

		public string FeatureName { get; set; }
	}

    public class VehicleFeatureMaper : Profile
    {
        public VehicleFeatureMaper()
        {
            CreateMap<VehicleFeature, DataSourceValue<int>>()
                .ForMember(desination => desination.Value, option => option.MapFrom(source => source.Id))
                .ForMember(desination => desination.DisplayName, option => option.MapFrom(source => source.FeatureName));
            CreateMap<DataSourceValue<int>, VehicleFeature>()
                .ForMember(desination => desination.Id, option => option.MapFrom(source => source.Value))
                .ForMember(desination => desination.FeatureName, option => option.MapFrom(source => source.DisplayName));
        }
    }

}
