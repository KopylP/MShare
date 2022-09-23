using System;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Filters;
using Proxy.Api;

namespace SpotifyProxy.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExceptionFilter]
    public class AlbumController : ControllerBase
	{
        private readonly IStreamingServiceClient _client;

        public AlbumController(IStreamingServiceClient client) => _client = client;

        [HttpGet("Url")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlbumResponseDto))]
        public async Task<IActionResult> GetByUrl([FromQuery] GetByUrlRequestDto model)
            => Ok(await _client.GetAlbumByUrl(model));
    }
}

