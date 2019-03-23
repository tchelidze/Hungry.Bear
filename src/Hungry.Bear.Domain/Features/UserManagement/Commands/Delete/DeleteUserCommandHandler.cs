using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ExecutionResult>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public DeleteUserCommandHandler(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExecutionResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var targetUser = await _userManager.FindByIdAsync(command.Id);
            targetUser.Deactivate();

            return ExecutionResult.Ok();
        }
    }
}