using System.Linq;
using Common.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnsekMeter.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly bool _shieldInternalMessages;

        public ApiExceptionFilterAttribute(bool shieldInternalMessages)
        {
            _shieldInternalMessages = shieldInternalMessages;
        }

        public override void OnException(ExceptionContext context)
        {
            var acceptHeader = context.HttpContext.Request.Headers["Accept"].FirstOrDefault()?.Split(',') ?? new string[0];
            if (!(acceptHeader.Any(x => x.StartsWith("application/json") || x.StartsWith("*/*"))))
            {
                base.OnException(context);
                return;
            }
            ErrorMessage result;

            if (context.Exception is ApiException apiException)
            {
                result = MapToMessage(apiException);
            }
            else
            {
                result = new ErrorMessage
                {
                    Error = 500,
                    Message = _shieldInternalMessages ? "Internal Error" : context.Exception.ToString()
                };
            }

            context.Result = new JsonResult(result)
            {
                StatusCode = result.Error
            };
            context.ExceptionHandled = true;
        }

        private static ErrorMessage MapToMessage(ApiException ex)
        {
            return new ErrorMessage
            {
                Error = ex.Error,
                Message = ex.Message,
                Details = ex.Details.Any() ? ex.Details.Select(MapToMessage).ToArray() : null
            };
        }
    }
}
