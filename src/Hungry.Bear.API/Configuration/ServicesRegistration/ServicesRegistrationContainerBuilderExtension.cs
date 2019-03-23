using Autofac;
using Microsoft.Extensions.Configuration;

namespace Hungry.Bear.API.Configuration.ServicesRegistration
{
    public static class ServicesRegistrationContainerBuilderExtension
    {
        public static void RegisterHungryBearServices(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .RegisterHungryBearDbSeeder();
        }

        public static ContainerBuilder RegisterHungryBearDbSeeder(this ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseSeeder>().InstancePerLifetimeScope();
            return builder;
        }
    }
}