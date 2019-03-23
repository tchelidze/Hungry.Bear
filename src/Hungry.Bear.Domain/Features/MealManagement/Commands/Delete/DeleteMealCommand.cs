using Hungry.Bear.Common.ExecutionResult;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Delete
{
    public class DeleteMealCommand : IRequest<ExecutionResult>
    {
        public int MealId { get; set; }
    }
}