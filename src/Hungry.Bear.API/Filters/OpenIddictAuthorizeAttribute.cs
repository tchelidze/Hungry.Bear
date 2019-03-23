using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenIddict.Validation;

namespace Hungry.Bear.API.Filters
{
    public class OpenIddictAuthorizeAttribute : AuthorizeAttribute, IOrderedFilter
    {
        public OpenIddictAuthorizeAttribute()
        {
            AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme;
        }

        public int Order { get; } = 1;
    }
}
