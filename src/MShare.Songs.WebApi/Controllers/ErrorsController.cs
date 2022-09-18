using System;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi;
using System.Net;

namespace MShare.Songs.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/errors")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [HttpGet("{code}")]
        public IActionResult Error(int code)
        {
            var parsedCode = (HttpStatusCode)code;
            var error = new ApiError(code, parsedCode.ToString());

            return new ObjectResult(error);
        }
    }
}

