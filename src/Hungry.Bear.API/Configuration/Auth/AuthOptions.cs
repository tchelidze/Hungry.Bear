namespace Hungry.Bear.API.Configuration.Auth
{
    public class AuthOptions
    {
        public string Audience { get; set; }

        public string Authority { get; set; }

        public string SigningKey { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public int AccessTokenLifetime { get; set; }
    }
}
