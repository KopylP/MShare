using System;
using Microsoft.AspNetCore.Http;

namespace MShare.Framework.Api.Exceptions
{
    public class ApiException : Exception
    {
        private static IDictionary<int, string> _statusCodes => new Dictionary<int, string>
        {
            [400] = "Bad Request",
            [401] = "Unauthorized",
            [404] = "Not Found",
            [500] = "Internal Server Error"
        };

        private static string GetDefaultStatusCode(int statusCode)
        {
            try
            {
                return _statusCodes[statusCode];
            }
            catch
            {
                return "";
            }
        }

        public int StatusCode { get; init; }

        public ApiException(int statusCode, string? message = null) : base(message ?? GetDefaultStatusCode(statusCode))
            => StatusCode = statusCode;
    }
}

