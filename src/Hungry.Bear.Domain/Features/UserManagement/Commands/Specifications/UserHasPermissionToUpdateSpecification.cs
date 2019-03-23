using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Hungry.Bear.Domain.Shared.Specifications;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Specifications
{
    public class UserHasPermissionToUpdateSpecificationInput
    {
        public string CurrentUserId { get; set; }

        public string TargetUserId { get; set; }
    }

    public class UserHasPermissionToUpdateSpecification : ISpecification<UserHasPermissionToUpdateSpecificationInput>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public UserHasPermissionToUpdateSpecification(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public virtual async Task<bool> IsSatisfiedByAsync(UserHasPermissionToUpdateSpecificationInput input)
        {
            var currentUser = await _userManager.FindByIdAsync(input.CurrentUserId);

            if (await _userManager.IsInRoleAsync(currentUser, RoleNames.Admin))
            {
                return true;
            }

            var targetUser = await _userManager.FindByIdAsync(input.TargetUserId);

            return !await _userManager.IsInRoleAsync(targetUser, RoleNames.Admin);
        }
    }
}