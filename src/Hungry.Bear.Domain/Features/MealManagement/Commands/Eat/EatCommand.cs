using Hungry.Bear.Common.ExecutionResult;
using MediatR;
using System;
using System.Collections.Generic;

namespace Hungry.Bear.Domain.Features.MealManagement.Commands.Eat
{
    public class EatCommand : IRequest<ExecutionResult<int>>
    {
        public string AuthorId { get; set; }

        public DateTime OccuredOn { get; set; }

        public IReadOnlyList<int> FoodIds { get; set; }
    }
}