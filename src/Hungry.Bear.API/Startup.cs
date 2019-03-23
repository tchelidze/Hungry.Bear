using Autofac;
using FluentValidation.AspNetCore;
using Hungry.Bear.API.Configuration;
using Hungry.Bear.API.Configuration.Auth;
using Hungry.Bear.API.Configuration.Mapping;
using Hungry.Bear.API.Configuration.MediatR;
using Hungry.Bear.API.Configuration.ServicesRegistration;
using Hungry.Bear.API.Shared;
using Hungry.Bear.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hungry.Bear.API
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterHungryBearServices(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<HungryBearIdentityDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetIdentityDbConnectionString());
                    options.UseOpenIddict();
                });

            services.AddScoped<DatabaseSeeder>();

            services.AddOpenIdAuth(Configuration, HostingEnvironment);

            services
                .AddMvc()
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            services.AddAutoMapperProfiles();
            services.AddConfiguredMediatR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (!env.IsDevelopment())
            {
                loggerFactory.AddAzureWebAppDiagnostics();
                loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Trace);
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}