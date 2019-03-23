using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Edit
{
    public class EditMealCommandHandler : IRequestHandler<EditMealCommand, ExecutionResult>
    {
        private readonly IMealRepository _mealRepository;

        public EditMealCommandHandler(
            IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<ExecutionResult> Handle(EditMealCommand command, CancellationToken cancellationToken)
        {
            return ExecutionResult.Ok();
        }
    }
}