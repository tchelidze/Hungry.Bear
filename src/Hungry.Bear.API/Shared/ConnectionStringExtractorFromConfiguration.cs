using Microsoft.Extensions.Configuration;

namespace Hungry.Bear.API.Shared
{
    public static class ConnectionStringExtractorFromConfiguration
    {
        public static string GetIdentityDbConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("IdentityDbConnectionString");
        }
    }
}