using AutoMapper;
using Domains.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models
{
    public class WarehouseOfCustomerModel
    {
        public int Id { get; set; }
        public string NameWarehouse { get; set; }
    }

    public class WarehouseOfCustomerModelMaper : Profile
    {
        public WarehouseOfCustomerModelMaper()
        {
            CreateMap<WareHouse, DataSourceValue<int>>()
                .ForMember(desination => desination.Value, option => option.MapFrom(source => source.Id))
                .ForMember(desination => desination.DisplayName, option => option.MapFrom(source => source.Address));
            CreateMap<DataSourceValue<int>, WareHouse>()
                .ForMember(desination => desination.Id, option => option.MapFrom(source => source.Value))
                .ForMember(desination => desination.Address, option => option.MapFrom(source => source.DisplayName));
        }
    }
}
