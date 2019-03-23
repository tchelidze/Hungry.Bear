using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Eat
{
    public class CreateRunCommandValidator : IPipelineBehavior<EatCommand, ExecutionResult<int>>
    {
        private readonly UserManager<HungryBearUser> _userManager;

        public CreateRunCommandValidator(UserManager<HungryBearUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ExecutionResult<int>> Handle(EatCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<ExecutionResult<int>> next)
        {
            if (await _userManager.FindByIdAsync(command.AuthorId) != null)
            {
                return ExecutionResult<int>.NotFound();
            }

            return await next();
        }
    }
}