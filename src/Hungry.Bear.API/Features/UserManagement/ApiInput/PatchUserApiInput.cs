using System.Linq;
using FluentValidation;
using Hungry.Bear.API.Features.Shared.Validation;
using Hungry.Bear.Domain.Features.UserManagement.Entities;

namespace Hungry.Bear.API.Features.UserManagement.ApiInput
{
    public class PutUserApiInput
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }

    public class PutUserApiInputValidator : AbstractValidator<PutUserApiInput>
    {
        public PutUserApiInputValidator()
        {
            RuleFor(it => it.Email).NotEmpty().EmailAddress();
            RuleFor(it => it.Password).NotEmpty();
            RuleFor(it => it.FirstName).NotEmpty();
            RuleFor(it => it.LastName).NotEmpty();
            RuleFor(it => it.PhoneNumber).NotEmpty().Matches(ValidationConstants.PhoneNumberValidatorRegEx);

            RuleFor(it => it.Role)
                .Must(it => RoleNames.All.Contains(it))
                .WithMessage("Invalid role")
                .Unless(it => it.Role == null);
        }
    }
}
