using System;
namespace MShare.Framework.WebApi.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; init; }

        public ApiException(int statusCode, string? message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

