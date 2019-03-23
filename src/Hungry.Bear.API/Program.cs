using Autofac.Extensions.DependencyInjection;
using Hungry.Bear.API.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Hungry.Bear.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var databaseSeeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
                databaseSeeder.SeedAsync().Wait();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost
                .CreateDefaultBuilder(args)
                .ConfigureServices(it => it.AddAutofac())
                .UseStartup<Startup>()
                .UseApplicationInsights();
        }
    }
}
