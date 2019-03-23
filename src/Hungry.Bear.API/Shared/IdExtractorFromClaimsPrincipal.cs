using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Hungry.Bear.API.Shared
{
    public static class IdExtractorFromClaimsPrincipal
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(JwtRegisteredClaimNames.Sub).Value;
        }
    }
}
