using Autofac;
using Autofac.Extensions.DependencyInjection;
using Groove.AspNetCore.IoC.Autofact;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceProvider BuildAutofactServiceProvider(this IServiceCollection services, Action<ServiceProviderOptions> setupAction = null)
        {
            // create a Autofac container builder
            var builder = new ContainerBuilder();

            // read service collection to Autofac
            builder.Populate(services);

            // use and configure Autofac
            Register(builder, setupAction);

            // build the Autofac container
            var container = builder.Build();

            // creating the IServiceProvider out of the Autofac container
            return new AutofacServiceProvider(container);
        }

        private static void Register(ContainerBuilder builder, Action<ServiceProviderOptions> setupAction)
        {
            if (setupAction == null)
            {
                return;
            }
            var providerOptions = new ServiceProviderOptions(builder);
            setupAction(providerOptions);
        }
    }
}
