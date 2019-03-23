using Hungry.Bear.DataAccess;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System.Threading.Tasks;

namespace Hungry.Bear.API.Configuration
{
    public class DatabaseSeeder
    {
        private readonly UserManager<HungryBearUser> _userManager;
        private readonly RoleManager<HungryBearUserRole> _roleManager;
        private readonly HungryBearIdentityDbContext _context;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _manager;

        public DatabaseSeeder(
            UserManager<HungryBearUser> userManager,
            HungryBearIdentityDbContext context,
            OpenIddictApplicationManager<OpenIddictApplication> manager,
            RoleManager<HungryBearUserRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _manager = manager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await SeedRolesAsync();
            await SeedUsersAsync();
            await SeedIdsClientsAsync();
        }

        private async Task SeedRolesAsync()
        {
            await EnsureRoleExistsAsync(RoleNames.NormalUser);
            await EnsureRoleExistsAsync(RoleNames.UserManager);
            await EnsureRoleExistsAsync(RoleNames.Admin);
        }

        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (await _roleManager.FindByNameAsync(roleName) == null)
            {
                await _roleManager.CreateAsync(new HungryBearUserRole { Name = roleName });
            }
        }

        private async Task SeedUsersAsync()
        {
            await EnsureUserExistsAsync("test@bearhungry.com", "aaAA11!!", RoleNames.NormalUser, "Normal", "User", "+995598774411");
            await EnsureUserExistsAsync("normal@bearhungry.com", "DSATdsat1234!@#$", RoleNames.NormalUser, "Normal", "User", "+995598774411");
            await EnsureUserExistsAsync("usermanager@bearhungry.com", "!@#$DSATdsat1234", RoleNames.UserManager, "Manager", "User", "+995598774433");
            await EnsureUserExistsAsync("admin@bearhungry.com", "DSATdsat!@#$1234", RoleNames.Admin, "Admin", "User", "+995598774422");
        }

        private async Task EnsureUserExistsAsync(string email, string password, string role, string firstName, string lastName, string phoneNumber)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return;
            }

            var user = new HungryBearUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task SeedIdsClientsAsync()
        {
            if (await _manager.FindByClientIdAsync("test_client") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "test_client",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    DisplayName = "Test client",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                        OpenIddictConstants.Permissions.Scopes.Roles
                    }
                };

                await _manager.CreateAsync(descriptor);
            }
        }
    }
}
