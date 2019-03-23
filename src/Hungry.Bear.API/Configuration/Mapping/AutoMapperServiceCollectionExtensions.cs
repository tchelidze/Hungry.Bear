using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Hungry.Bear.API.Configuration.Mapping
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            void AutoMapperConfiguration(IMapperConfigurationExpression opts)
            {
                opts.AddProfiles(typeof(Startup).Assembly);
                opts.CreateMissingTypeMaps = true;
            }

            services.AddAutoMapper(AutoMapperConfiguration);
            Mapper.Initialize(AutoMapperConfiguration);

            return services;
        }
    }
}