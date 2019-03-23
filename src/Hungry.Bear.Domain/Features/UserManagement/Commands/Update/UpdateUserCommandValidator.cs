using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Specifications;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Domain.Shared.Specifications;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Update
{
    public class UpdateUserCommandValidator : IPipelineBehavior<UpdateUserCommand, ExecutionResult>
    {
        private readonly UserHasPermissionToGrantRoleSpecification _userHasPermissionToGrantRoleSpecification;
        private readonly UserHasPermissionToUpdateSpecification _userHasPermissionToUpdateSpecification;
        private readonly UserManager<HungryBearUser> _userManager;

        public UpdateUserCommandValidator(
            UserHasPermissionToGrantRoleSpecification userHasPermissionToGrantRoleSpecification,
            UserHasPermissionToUpdateSpecification userHasPermissionToUpdateSpecification,
            UserManager<HungryBearUser> userManager)
        {
            _userHasPermissionToGrantRoleSpecification = userHasPermissionToGrantRoleSpecification;
            _userHasPermissionToUpdateSpecification = userHasPermissionToUpdateSpecification;
            _userManager = userManager;
        }

        public async Task<ExecutionResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken,
            RequestHandlerDelegate<ExecutionResult> next)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user == null)
            {
                return ExecutionResult.NotFound();
            }

            if (await _userHasPermissionToUpdateSpecification.IsNotSatisfiedByAsync(
                new UserHasPermissionToUpdateSpecificationInput
                {
                    CurrentUserId = command.CurrentUserId,
                    TargetUserId = command.Id
                }))
            {
                return ExecutionResult.Forbidden();
            }

            if (await _userHasPermissionToGrantRoleSpecification.IsNotSatisfiedByAsync(
                new CurrentUserHasPermissionToGrantRoleSpecificationInput
                {
                    Role = command.Role,
                    UserId = command.CurrentUserId
                }))
            {
                return ExecutionResult.Forbidden();
            }

            return await next();
        }
    }
}