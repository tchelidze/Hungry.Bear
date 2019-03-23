using Hungry.Bear.Common.ExecutionResult;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hungry.Bear.API.Shared
{
    public static class ExecutionResultToIActionResultConverter
    {
        public static IActionResult ToActionResult(this ExecutionResult executionResult)
        {
            if (executionResult.IsValid())
            {
                return new OkResult();
            }

            if (executionResult.Message != null)
            {
                return new ObjectResult(executionResult.Message)
                {
                    StatusCode = (int)StatusCodeFromExecutionTypeResult(executionResult.ResultType)
                };
            }

            return new StatusCodeResult((int)StatusCodeFromExecutionTypeResult(executionResult.ResultType));
        }

        public static HttpStatusCode StatusCodeFromExecutionTypeResult(ExecutionResultType type)
        {
            switch (type)
            {
                case ExecutionResultType.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case ExecutionResultType.Forbidden:
                    return HttpStatusCode.Forbidden;
                case ExecutionResultType.Conflict:
                    return HttpStatusCode.Conflict;
                case ExecutionResultType.UpstreamError:
                    return HttpStatusCode.BadGateway;
                case ExecutionResultType.UpstreamErrorNoResponse:
                    return HttpStatusCode.GatewayTimeout;
                case ExecutionResultType.ValidationError:
                    return HttpStatusCode.BadRequest;
                case ExecutionResultType.Exception:
                    return HttpStatusCode.InternalServerError;
                case ExecutionResultType.NotFound:
                    return HttpStatusCode.NotFound;
                case ExecutionResultType.NoContent:
                    return HttpStatusCode.NoContent;

                default:
                    return HttpStatusCode.OK;
            }
        }
    }
}