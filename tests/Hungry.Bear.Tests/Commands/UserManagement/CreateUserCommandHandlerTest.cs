using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Create;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Hungry.Bear.Tests.Commands.UserManagement
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<UserManager<HungryBearUser>> _userManagerMock;

        public CreateUserCommandHandlerTest()
        {
            _userManagerMock = new Mock<UserManager<HungryBearUser>>(new Mock<IUserStore<HungryBearUser>>().Object, null, null, null, null, null, null, null, null);
        }

        private CreateUserCommandHandler _sut;

        private CreateUserCommandHandler Sut
            => _sut ?? (_sut = new CreateUserCommandHandler(_userManagerMock.Object));


        public static CreateUserCommand ValidSampleCreateUserCommand = new CreateUserCommand
        {
            PhoneNumber = "+995598454748",
            Email = "someemail@gmail.com",
            FirstName = "Bitchiko",
            LastName = "Tchelidze",
            Password = "dsatDSAT11234!@#$e)"
        };

        public HungryBearUser CreatedFrom(CreateUserCommand command)
        {
            return new HungryBearUser
            {
                Email = command.Email,
                UserName = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber
            };
        }

        [Fact]
        public async Task user_should_be_created_with_normal_user_role_and_return_ok()
        {
            HungryBearUser userPassedIntoCreateAsync = null;

            var newUserId = "daaasas";

            _userManagerMock
                .Setup(it => it.CreateAsync(It.IsAny<HungryBearUser>(), ValidSampleCreateUserCommand.Password))
                .Callback((HungryBearUser user, string password) => { user.Id = newUserId; userPassedIntoCreateAsync = user; })
                .Returns(Task.FromResult(IdentityResult.Success));

            _userManagerMock
                .Setup(it => it.AddToRoleAsync(It.IsAny<HungryBearUser>(), RoleNames.NormalUser))
                .Returns(Task.FromResult(IdentityResult.Success));

            var result = await Sut.Handle(ValidSampleCreateUserCommand, CancellationToken.None);

            result.ResultType.ShouldBe(ExecutionResultType.Ok);
            result.Value.ShouldBe(newUserId);

            userPassedIntoCreateAsync.Email.ShouldBe(ValidSampleCreateUserCommand.Email);
            userPassedIntoCreateAsync.FirstName.ShouldBe(ValidSampleCreateUserCommand.FirstName);
            userPassedIntoCreateAsync.LastName.ShouldBe(ValidSampleCreateUserCommand.LastName);
            userPassedIntoCreateAsync.PhoneNumber.ShouldBe(ValidSampleCreateUserCommand.PhoneNumber);

            _userManagerMock.Verify(it => it.AddToRoleAsync(userPassedIntoCreateAsync, RoleNames.NormalUser));
        }
    }
}
