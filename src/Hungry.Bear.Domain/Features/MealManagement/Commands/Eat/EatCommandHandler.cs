using System.Threading;
using System.Threading.Tasks;
using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Entities;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Eat
{
    public class EatCommandHandler : IRequestHandler<EatCommand, ExecutionResult<int>>
    {
        private readonly IMealRepository _mealRepository;

        public EatCommandHandler(
            IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<ExecutionResult<int>> Handle(EatCommand command, CancellationToken cancellationToken)
        {
            var newMeal = new Meal
            {
            };


            return ExecutionResult<int>.Ok(0);
        }
    }
}