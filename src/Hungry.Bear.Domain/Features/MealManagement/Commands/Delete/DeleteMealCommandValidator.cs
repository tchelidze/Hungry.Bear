using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Delete
{
    public class DeleteMealCommandValidator : IPipelineBehavior<DeleteMealCommand, ExecutionResult>
    {
        private readonly IMealRepository _mealRepository;

        public DeleteMealCommandValidator(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<ExecutionResult> Handle(DeleteMealCommand command, CancellationToken cancellationToken,
            RequestHandlerDelegate<ExecutionResult> next)
        {
            return await next();
        }
    }
}