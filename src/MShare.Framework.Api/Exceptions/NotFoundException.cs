using Microsoft.AspNetCore.Http;

namespace MShare.Framework.Api.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string? message = default) : base(StatusCodes.Status404NotFound, message)
        {
        }

        public static void ThrowIf(bool condition, string? message = default)
        {
            if (condition)
                throw new NotFoundException(message);
        }
    }
}

