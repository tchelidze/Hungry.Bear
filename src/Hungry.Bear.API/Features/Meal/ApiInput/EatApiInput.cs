using System;
using FluentValidation;
using Hungry.Bear.Domain.Shared.Validation;

namespace Hungry.Bear.API.Features.Meal.ApiInput
{
    public class EatApiInput
    {
        public DateTime OccuredOn { get; set; }
    }

    public class EatApiInputValidator : AbstractValidator<EatApiInput>
    {
        public EatApiInputValidator()
        {
            RuleFor(it => it.OccuredOn).SetValidator(new DateTimeValidator());
        }
    }
}