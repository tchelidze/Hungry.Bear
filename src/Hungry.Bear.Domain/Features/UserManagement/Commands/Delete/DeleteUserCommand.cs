using Hungry.Bear.Common.ExecutionResult;
using MediatR;

namespace Hungry.Bear.Domain.Features.UserManagement.Commands.Delete
{
    public class DeleteUserCommand : IRequest<ExecutionResult>
    {
        public string Id { get; set; }

        public string CurrentUserId { get; set; }
    }
}
