using System.Collections.Generic;

namespace Hungry.Bear.Domain.Features.UserManagement.Entities
{
    public static class RoleNames
    {
        public const string NormalUser = nameof(NormalUser);

        public const string UserManager = nameof(UserManager);

        public const string Admin = nameof(Admin);

        public static readonly IReadOnlyList<string> All = new List<string>
        {
            NormalUser,
            UserManager,
            Admin
        };
    }
}