using System;
using System.Collections.Generic;
using Hungry.Bear.Common.ExecutionResult;

namespace Hungry.Bear.Tests.Shared
{
    public class ExecutionResultsColllection
    {
        public static IReadOnlyList<ExecutionResult> FailedExecutionResults = new List<ExecutionResult>()
        {
            ExecutionResult.ValidationError(new ExecutionMessage()),
            ExecutionResult.Conflict(new ExecutionMessage()),
            ExecutionResult.Exception(new InvalidOperationException("A")),
            ExecutionResult.Forbidden(new ExecutionMessage()),
            ExecutionResult.NoContent(),
            ExecutionResult.ValidationError(new ExecutionMessage()),
            ExecutionResult.NotFound(new ExecutionMessage()),
            ExecutionResult.NotImplemented(),
            ExecutionResult.Unauthorized(new ExecutionMessage()),
            ExecutionResult.UpstreamError(false,new ExecutionMessage())
        };

        public static ExecutionResult GetRandomFailedExecutionResult()
        {
            return FailedExecutionResults[new Random().Next(0, FailedExecutionResults.Count - 1)];
        }

        public static ExecutionResult<TValue> GetRandomFailedExecutionResult<TValue>()
        {
            var randomFailedExecutionResult = GetRandomFailedExecutionResult();
            return new ExecutionResult<TValue>(randomFailedExecutionResult.ResultType, randomFailedExecutionResult.Message);
        }
    }
}
