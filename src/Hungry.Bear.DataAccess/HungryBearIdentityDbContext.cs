using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Hungry.Bear.DataAccess
{
    public class HungryBearIdentityDbContext : IdentityDbContext<HungryBearUser, HungryBearUserRole, string>
    { }
}