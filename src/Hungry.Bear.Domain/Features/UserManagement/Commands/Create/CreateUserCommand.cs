using Hungry.Bear.Common.ExecutionResult;
using MediatR;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Create
{
    public class CreateUserCommand : IRequest<ExecutionResult<string>>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
