using Groove.AspNetCore.Mvc.Jwt;
using Groove.AspNetCore.Mvc.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static AuthenticationBuilder AddJwtBearerAuthentication(this IServiceCollection services, Action<JwtTokenOptions> options)
        {
            var optInstance = new JwtTokenOptions();
            options(optInstance);

            services.AddSingleton(optInstance);
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            return services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = optInstance.ValidateAudience,
                    ValidateIssuer = optInstance.ValidateIssuer,
                    ValidateIssuerSigningKey = optInstance.ValidateIssuerSigningKey,
                    ValidateLifetime = optInstance.ValidateLifetime,
                    IssuerSigningKey = optInstance.SecurityKey
                };
            });
        }
    }
}
