using Groove.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Mvc
{
    public static class ServiceCollectionExtentions
    {
        public static IMvcBuilder AddGrooveMvc(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvc(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });

            mvcBuilder.AddJsonOptions(x =>
            {
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            });

            return mvcBuilder;
        }
        public static IMvcCoreBuilder AddGrooveMvcApi(this IServiceCollection services)
        {
            var mvcBuilder = services.AddMvcCore(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });
            mvcBuilder.AddApiExplorer();    // for API metadata explorer, which are heplful when you use swagger
            mvcBuilder.AddJsonFormatters(); // for http response
            mvcBuilder.AddDataAnnotations(); // for validator
            mvcBuilder.AddAuthorization(); 
            mvcBuilder.AddCors(options =>
            {
                options.AddPolicy(CorsPolicies.AllowAny, builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            mvcBuilder.AddJsonOptions(x =>
            {
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            });

            return mvcBuilder;
        }
    }
}
