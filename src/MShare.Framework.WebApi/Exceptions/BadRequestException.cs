using System;
using Microsoft.AspNetCore.Http;

namespace MShare.Framework.WebApi.Exceptions
{
	public class BadRequestException : ApiException
    {
        public BadRequestException(string? message = default) : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public static void Throw(string? message = default) => throw new BadRequestException(message);

        public static void ThrowIf(bool condition, string? message = default)
        {
            if (condition)
                Throw(message);
        }
    }
}

