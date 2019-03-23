using Hungry.Bear.Common.ExecutionResult;
using MediatR;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Edit
{
    public class EditMealCommand : IRequest<ExecutionResult>
    {
        public int MealId { get; set; }
    }
}