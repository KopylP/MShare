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
                context.Result = new ObjectResult(apiException.Message)
                {
                    StatusCode = apiException.StatusCode
                };
            }
        }
    }
}