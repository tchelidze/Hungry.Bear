using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Edit
{
    public class EditMealCommandValidator : IPipelineBehavior<EditMealCommand, ExecutionResult>
    {
        private readonly IMealRepository _mealRepository;

        public EditMealCommandValidator(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<ExecutionResult> Handle(EditMealCommand command, CancellationToken cancellationToken,
            RequestHandlerDelegate<ExecutionResult> next)
        {
            return await next();
        }
    }
}