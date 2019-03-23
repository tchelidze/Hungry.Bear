using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Delete
{
    public class DeleteMealCommandHandler : IRequestHandler<DeleteMealCommand, ExecutionResult>
    {
        private readonly IMealRepository _mealRepository;

        public DeleteMealCommandHandler(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<ExecutionResult> Handle(DeleteMealCommand command, CancellationToken cancellationToken)
        {
            return ExecutionResult.Ok();
        }
    }
}