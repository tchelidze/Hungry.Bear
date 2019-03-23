using System;

namespace Hungry.Bear.Common.ExecutionResult
{
    public class ExecutionResult
    {
        public ExecutionResult(ExecutionResultType result, ExecutionMessage message = null)
        {
            ResultType = result;

            if (message != null)
            {
                Message = message;
            }
        }

        protected ExecutionResult()
        {
        }

        public ExecutionResultType ResultType { get; protected set; }

        public ExecutionMessage Message { get; protected set; }

        public bool IsValid()
        {
            return (int)ResultType <= (int)ExecutionResultType.Ok;
        }

        public bool IsFailure()
        {
            return !IsValid();
        }

        public static ExecutionResult Ok()
        {
            return new ExecutionResult
            {
                ResultType = ExecutionResultType.Ok
            };
        }

        public static ExecutionResult Unauthorized(ExecutionMessage message)
        {
            var result = new ExecutionResult(ExecutionResultType.Unauthorized)
            {
                Message = message
            };

            return result;
        }

        public static ExecutionResult Forbidden(ExecutionMessage message = null)
        {
            var result = new ExecutionResult(ExecutionResultType.Forbidden)
            {
                Message = message
            };

            return result;
        }

        public static ExecutionResult UpstreamError(bool didRespond, ExecutionMessage message)
        {
            return new ExecutionResult
            {
                Message = message,
                ResultType = didRespond
                    ? ExecutionResultType.UpstreamError
                    : ExecutionResultType.UpstreamErrorNoResponse
            };
        }

        public static ExecutionResult NotImplemented()
        {
            return new ExecutionResult(ExecutionResultType.NotImplemented);
        }

        public static ExecutionResult NoContent()
        {
            return new ExecutionResult(ExecutionResultType.NoContent);
        }

        public static ExecutionResult NotFound(ExecutionMessage message = null)
        {
            var result = new ExecutionResult(ExecutionResultType.NotFound)
            {
                Message = message
            };

            return result;
        }

        public static ExecutionResult ValidationError(ExecutionMessage message = null)
        {
            return new ExecutionResult
            {
                Message = message,
                ResultType = ExecutionResultType.ValidationError
            };
        }

        public static ExecutionResult Conflict(ExecutionMessage message)
        {
            return new ExecutionResult
            {
                Message = message,
                ResultType = ExecutionResultType.Conflict
            };
        }

        public static ExecutionResult Exception(Exception ex)
        {
            return new ExecutionResult
            {
                Message = new ExecutionMessage
                {
                    Code = ErrorCodes.UNHANDLED_EXCEPTION,
                    Message = ex.Message
                },
                ResultType = ExecutionResultType.Exception
            };
        }
    }

    public class ExecutionResult<T> : ExecutionResult
    {
        public ExecutionResult(ExecutionResult result, T value = default(T))
        {
            Message = result.Message;
            ResultType = result.ResultType;
            Value = value;
        }

        public ExecutionResult(ExecutionResultType result, ExecutionMessage message = null,
            T value = default(T))
        {
            ResultType = result;
            Value = value;
            Message = message;
        }

        protected ExecutionResult()
        {
        }

        public T Value { get; private set; }

        public static ExecutionResult<T> Ok(T value)
        {
            return new ExecutionResult<T> { Value = value, ResultType = ExecutionResultType.Ok };
        }

        public static new ExecutionResult<T> Unauthorized(ExecutionMessage message)
        {
            var result = ExecutionResult.Unauthorized(message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> Forbidden(ExecutionMessage message = null)
        {
            var result = ExecutionResult.Forbidden(message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> UpstreamError(bool didRespond, ExecutionMessage message)
        {
            var result = ExecutionResult.UpstreamError(didRespond, message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> NotFound(ExecutionMessage message = null)
        {
            var result = ExecutionResult.NotFound(message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> ValidationError(ExecutionMessage message = null)
        {
            var result = ExecutionResult.ValidationError(message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> Conflict(ExecutionMessage message)
        {
            var result = ExecutionResult.Conflict(message);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }

        public static new ExecutionResult<T> Exception(Exception ex)
        {
            var result = ExecutionResult.Exception(ex);
            return new ExecutionResult<T> { ResultType = result.ResultType, Message = result.Message };
        }
    }
}