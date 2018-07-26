using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Groove.AspNetCore.DataBinding.AutoMapperExtentions
{
    public static class IMapperExtentions
    {
        public static IQueryable<TDestination> MapQueryTo<TDestination>(this IMapper mapper, IQueryable source)
        {
            return source.ProjectTo<TDestination>(mapper.ConfigurationProvider);
        }

        public static IQueryable<TDestination> MapQueryTo<TDestination>(this IQueryable source, IMapper mapper)
        {
            return source.ProjectTo<TDestination>(mapper.ConfigurationProvider);
        }

    }
}
