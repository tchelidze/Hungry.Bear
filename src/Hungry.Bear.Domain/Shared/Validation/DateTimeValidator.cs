using System;
using FluentValidation.Validators;

namespace Hungry.Bear.Domain.Shared.Validation
{
    public class DateTimeValidator : PropertyValidator
    {
        public DateTimeValidator() : base($"Value must be within {MinValue} - {MaxValue} range.")
        { }

        private static readonly DateTime MinValue = new DateTime(1753, 1, 1);

        private static readonly DateTime MaxValue = DateTime.MaxValue;

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!(context.PropertyValue is DateTime valueAsDateTime))
            {
                return false;
            }

            if (valueAsDateTime < MinValue)
            {
                return false;
            }

            if (valueAsDateTime > MaxValue)
            {
                return false;
            }

            return true;
        }
    }
}
