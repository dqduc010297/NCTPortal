using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domains.Identity.Entities;
using FluentValidation.AspNetCore;
using Groove.AspNetCore.Mvc;
using Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace GoGoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");
            var jwtSecurityKey = Configuration.GetValue<string>("Security:Jwt:SecurityKey");
            var tokenTimeOutMinutes = Configuration.GetValue<long>("Security:Jwt:TokenTimeOutMinutes");

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(defaultConnectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });


            services.AddGrooveMvcApi().AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<Domains.AssemplyMarker>().RegisterValidatorsFromAssemblyContaining<GoGoApi.Startup>());
            //services.AddCors();
            services.AddAutoMapper(typeof(Domains.AssemplyMarker));

            // Add Kendo UI services to the services container
            services.AddKendo();

            // Add UoW 
            services.AddUnitOfWork<ApplicationDbContext>();

            // Add Identity
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); // protection provider responsible for generating an email confirmation token or a password reset token
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;


                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(tokenTimeOutMinutes);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

            });

            // Add Jwt Bearer
            // IMPORTANCE: AddJwtBearerAuthentication should be added after services.AddIdentity, to replaced Authentication config in Identity
            services.AddJwtBearerAuthentication(options =>
            {
                options.SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));
                options.TokenTimeOutMinutes = tokenTimeOutMinutes;
            });

            // ALL SERVICE REGISTERS SHOULD BE PLACED BEFORE THIS LINE
            // Register our custom service provider
            var autofactServiceProvider = services.BuildAutofactServiceProvider(options =>
            {
                // Register services,...
                options.AddAsImplementedInterfaces(typeof(Domains.AssemplyMarker));
                // Register repositories
                options.AddAsImplementedInterfaces(typeof(Infrastructures.AssemplyMarker));
            });

            return autofactServiceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
                   builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
           );
            //app.UseCors(CorsPolicies.AllowAny);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
#pragma warning disable CS0618 // Type or member is obsolete
            app.UseKendo(env);
#pragma warning restore CS0618 // Type or member is obsolete
            app.UseMvc();
        }
    }
}
