using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Hungry.Bear.Tests.Shared
{
    public class UserManagerMockFactory
    {
        public static Mock<UserManager<HungryBearUser>> Create(Mock<IUserStore<HungryBearUser>> userStoreMock)
        {
            return new Mock<UserManager<HungryBearUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        public static Mock<UserManager<HungryBearUser>> Create()
        {
            return Create(new Mock<IUserStore<HungryBearUser>>());
        }
    }
}
