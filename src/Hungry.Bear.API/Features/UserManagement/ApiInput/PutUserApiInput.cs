using System.Linq;
using FluentValidation;
using Hungry.Bear.API.Features.Shared.Validation;
using Hungry.Bear.Domain.Features.UserManagement.Entities;

namespace Hungry.Bear.API.Features.UserManagement.ApiInput
{
    public class PatchUserApiInput
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }

    public class PatchUserApiInputValidator : AbstractValidator<PatchUserApiInput>
    {
        public PatchUserApiInputValidator()
        {
            RuleFor(it => it.Email)
                .NotEmpty()
                .EmailAddress()
                .Unless(it => it.Email == null);

            RuleFor(it => it.Password)
                .NotEmpty()
                .Unless(it => it.Password == null);

            RuleFor(it => it.FirstName)
                .NotEmpty()
                .Unless(it => it.FirstName == null);

            RuleFor(it => it.LastName)
                .NotEmpty()
                .Unless(it => it.LastName == null);

            RuleFor(it => it.PhoneNumber)
                .Matches(ValidationConstants.PhoneNumberValidatorRegEx)
                .Unless(it => it.PhoneNumber == null);

            RuleFor(it => it.Role)
                .Must(it => RoleNames.All.Contains(it))
                .WithMessage("Invalid role")
                .Unless(it => it.Role == null);
        }
    }
}
