using Hungry.Bear.Domain.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hungry.Bear.Domain.Features.UserManagement.Entities
{
    public class HungryBearUser : IdentityUser<string>, IHasId<string>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}