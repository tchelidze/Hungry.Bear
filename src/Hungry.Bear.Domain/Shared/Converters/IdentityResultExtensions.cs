using Microsoft.AspNetCore.Identity;

namespace Hungry.Bear.Domain.Shared.Converters
{
    public static class IdentityResultExtensions
    {
        public static bool IsFailure(this IdentityResult result)
        {
            return !result.Succeeded;
        }
    }
}
