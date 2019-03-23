using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Hungry.Bear.Domain.Shared.Converters;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ExecutionResult>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public UpdateUserCommandHandler(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExecutionResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var targetUser = await _userManager.FindByIdAsync(command.Id);
            var targetUserRole = (await _userManager.GetRolesAsync(targetUser)).First();

            targetUser.Email = command.Email ?? targetUser.Email;
            targetUser.UserName = command.Email ?? targetUser.UserName;
            targetUser.FirstName = command.FirstName ?? targetUser.FirstName;
            targetUser.LastName = command.LastName ?? targetUser.LastName;
            targetUser.PhoneNumber = command.PhoneNumber ?? targetUser.PhoneNumber;

            if (command.Role != null && command.Role != targetUserRole)
            {
                var removeFromRoleResult = await _userManager.RemoveFromRoleAsync(targetUser, targetUserRole);

                if (removeFromRoleResult.IsFailure())
                {
                    return removeFromRoleResult.ToExecutionResult();
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(targetUser, command.Role);

                if (addToRoleResult.IsFailure())
                {
                    return addToRoleResult.ToExecutionResult();
                }
            }

            if (command.Password != null)
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(targetUser);

                if (removePasswordResult.IsFailure())
                {
                    return removePasswordResult.ToExecutionResult();
                }

                var addPasswordResult = await _userManager.AddPasswordAsync(targetUser, command.Password);

                if (addPasswordResult.IsFailure())
                {
                    return addPasswordResult.ToExecutionResult();
                }
            }

            var updateUserResult = await _userManager.UpdateAsync(targetUser);

            if (updateUserResult.IsFailure())
            {
                return updateUserResult.ToExecutionResult();
            }

            return ExecutionResult.Ok();
        }
    }
}