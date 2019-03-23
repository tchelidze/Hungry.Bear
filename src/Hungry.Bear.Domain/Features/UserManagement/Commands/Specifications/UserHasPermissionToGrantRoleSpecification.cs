using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Hungry.Bear.Domain.Shared.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Specifications
{
    public class CurrentUserHasPermissionToGrantRoleSpecificationInput
    {
        public string Role { get; set; }

        public string UserId { get; set; }
    }

    public class UserHasPermissionToGrantRoleSpecification : ISpecification<CurrentUserHasPermissionToGrantRoleSpecificationInput>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public UserHasPermissionToGrantRoleSpecification(
            UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public virtual async Task<bool> IsSatisfiedByAsync(CurrentUserHasPermissionToGrantRoleSpecificationInput input)
        {
            if (input.Role != RoleNames.Admin)
            {
                return true;
            }

            var currentUser = await _userManager.FindByIdAsync(input.UserId);
            return await _userManager.IsInRoleAsync(currentUser, RoleNames.Admin);
        }
    }
}