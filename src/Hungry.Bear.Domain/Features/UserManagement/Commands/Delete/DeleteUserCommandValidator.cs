using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Delete
{
    public class DeleteUserCommandValidator : IPipelineBehavior<DeleteUserCommand, ExecutionResult>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public DeleteUserCommandValidator(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExecutionResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult> next)
        {
            if (await _userManager.FindByIdAsync(command.Id) != null)
            {
                return ExecutionResult.NotFound();
            }

            if (command.Id == command.CurrentUserId)
            {
                return ExecutionResult.ValidationError(new ExecutionMessage
                {
                    Code = ErrorCodes.USER_ISNT_ALLOWED_TO_DELETE_ITS_ACCOUNT,
                    Target = nameof(command.Id),
                    Message = "User isn't allowed to delete its accounts"
                });
            }

            return await next();
        }
    }
}