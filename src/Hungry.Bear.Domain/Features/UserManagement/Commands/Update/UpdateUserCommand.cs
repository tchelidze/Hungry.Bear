using Hungry.Bear.Common.ExecutionResult;
using MediatR;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Update
{
    public class UpdateUserCommand : IRequest<ExecutionResult>
    {
        public string CurrentUserId { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
