using System;
using Microsoft.AspNetCore.Http;

namespace MShare.Framework.Api.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string? message = default) : base(StatusCodes.Status401Unauthorized, message)
        {
        }
    }
}

