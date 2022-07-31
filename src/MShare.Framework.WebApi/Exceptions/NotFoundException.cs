using Microsoft.AspNetCore.Http;

namespace MShare.Framework.WebApi.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string? message = default) : base(StatusCodes.Status404NotFound, message)
        {
        }
    }
}

