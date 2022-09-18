using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MShare.Framework.WebApi.Exceptions;

namespace MShare.Framework.WebApi.Filters
{
    public class ApiExceptionFilterAttribute: Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is ApiException apiException)
            {
                var parsedCode = (HttpStatusCode)apiException.StatusCode;
                context.Result = new ObjectResult(new ApiError(apiException.StatusCode, parsedCode.ToString(), exception.Message))
                {
                    StatusCode = apiException.StatusCode
                };
            }
        }
    }
}