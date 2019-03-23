using FluentValidation;
using Hungry.Bear.API.Features.Shared.Validation;

namespace Hungry.Bear.API.Features.UserManagement.ApiInput
{
    public class CreateUserApiInput
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class CreateUserApiInputValidator : AbstractValidator<CreateUserApiInput>
    {
        public CreateUserApiInputValidator()
        {
            RuleFor(it => it.Email).NotEmpty().EmailAddress();
            RuleFor(it => it.Password).NotEmpty();
            RuleFor(it => it.FirstName).NotEmpty();
            RuleFor(it => it.LastName).NotEmpty();
            RuleFor(it => it.PhoneNumber).NotEmpty().Matches(ValidationConstants.PhoneNumberValidatorRegEx);
        }
    }
}
