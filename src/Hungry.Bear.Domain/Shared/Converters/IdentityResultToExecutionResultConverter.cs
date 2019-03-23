using System.Linq;
using Hungry.Bear.Common.ExecutionResult;
using Microsoft.AspNetCore.Identity;

namespace Hungry.Bear.Domain.Shared.Converters
{
    public static class IdentityResultToExecutionResultConverter
    {
        public static ExecutionResult ToExecutionResult(this IdentityResult identityResult)
        {
            if (identityResult.Succeeded)
            {
                return ExecutionResult.Ok();
            }

            if (identityResult.Errors.Any())
            {
                var firstIdentityError = identityResult.Errors.First();

                return ExecutionResult<string>.ValidationError(new ExecutionMessage
                {
                    Code = firstIdentityError.Code,
                    Message = firstIdentityError.Description
                });
            }

            return ExecutionResult<string>.ValidationError();
        }

        public static ExecutionResult<TValue> ToFailedExecutionResult<TValue>(this IdentityResult identityResult)
        {
            var executionResult = identityResult.ToExecutionResult();
            return new ExecutionResult<TValue>(executionResult.ResultType, executionResult.Message);
        }
    }
}
