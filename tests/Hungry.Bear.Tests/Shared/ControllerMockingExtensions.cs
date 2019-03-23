using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hungry.Bear.Tests.Shared
{
    public static class ControllerMockingExtensions
    {
        public static void SetUser(this ControllerBase controller, int userId)
        {
            typeof(ControllerBase)
                .GetProperty("ControllerContext", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(controller, new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                        }))
                    }
                });
        }
    }
}
