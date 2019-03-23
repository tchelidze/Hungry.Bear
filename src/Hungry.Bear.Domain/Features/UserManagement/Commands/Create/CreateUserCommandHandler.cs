using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Hungry.Bear.Domain.Shared.Converters;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ExecutionResult<string>>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public CreateUserCommandHandler(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExecutionResult<string>> Handle(CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var newUser = new HungryBearUser
            {
                Email = command.Email,
                UserName = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber
            };

            var createUserResult = await _userManager.CreateAsync(newUser, command.Password);

            if (createUserResult.IsFailure())
            {
                return createUserResult.ToFailedExecutionResult<string>();
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, RoleNames.NormalUser);

            if (addToRoleResult.IsFailure())
            {
                return addToRoleResult.ToFailedExecutionResult<string>();
            }

            return ExecutionResult<string>.Ok(newUser.Id);
        }
    }
}