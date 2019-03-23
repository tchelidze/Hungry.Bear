using System.Collections.Generic;
using Hungry.Bear.API.Shared;
using Hungry.Bear.Common.ExecutionResult;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace Hungry.Bear.Tests.Shared
{
    public static class IActionResultAssertionExtensions
    {
        public static void ShouldBeCreatedBasedOn(this IActionResult result, ExecutionResult source)
        {
            var correctActionResult = source.ToActionResult();

            if (correctActionResult is OkResult)
            {
                result.ShouldBeOfType(typeof(OkResult));
            }
            else if (correctActionResult is ObjectResult correctObjectResult)
            {
                result.ShouldBeOfType(typeof(ObjectResult));
                var resultAsObjectResult = (ObjectResult)result;
                resultAsObjectResult.StatusCode.ShouldBe(((ObjectResult)correctActionResult).StatusCode);

                if (resultAsObjectResult.Value is Dictionary<string, string[]>)
                {
                    var value = (Dictionary<string, string[]>)resultAsObjectResult.Value;
                    var correctValue = (Dictionary<string, string[]>)correctObjectResult.Value;
                    value.Keys.ShouldBe(correctValue.Keys);

                    foreach (var correctValueKey in correctValue.Keys)
                    {
                        value[correctValueKey].ShouldBe(correctValue[correctValueKey]);
                    }
                }
            }
            else
            {
                result.ShouldBeOfType(typeof(StatusCodeResult));
                (result as StatusCodeResult).StatusCode.ShouldBe((int)ExecutionResultToIActionResultConverter.StatusCodeFromExecutionTypeResult(source.ResultType));
            }
        }
    }
}
