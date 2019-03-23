using AutoMapper;
using Hungry.Bear.API.Features.Meal.Controllers;

namespace Hungry.Bear.Tests.Shared
{
    public static class MapperFactory
    {
        static MapperFactory()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(MealsManagementController));
                cfg.CreateMissingTypeMaps = true;
            });
        }

        public static IMapper Create()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(MealsManagementController));
                cfg.CreateMissingTypeMaps = true;
            });

            return config.CreateMapper();
        }
    }
}
