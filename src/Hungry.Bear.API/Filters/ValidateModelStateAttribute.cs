using Hungry.Bear.Common.ExecutionResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hungry.Bear.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidateModelStateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
            {
                await next();
            }

            if (context.ModelState.All(it => !it.Value.Errors.Any()))
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.BadRequest);
                return;
            }

            var firstFailureModelState = context
                .ModelState
                .FirstOrDefault(it => it.Value.Errors.Any());

            context.Result = new ObjectResult(new ExecutionMessage
            {
                Message = firstFailureModelState.Value.Errors.First().ErrorMessage,
                Code = ErrorCodes.VALIDATION_ERROR
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}