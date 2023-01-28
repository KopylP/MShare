using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Filters;
using Proxy.Api;
using Unidecode.NET;

namespace AppleProxy.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExceptionFilter]
    public class SongController : ControllerBase
    {
        private const int MAX_LIMIT = 8;
        private const int MAX_TAKE = 5;

        private readonly IStreamingServiceClient _client;

        public SongController(IStreamingServiceClient client) => _client = client;

        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongResponseDto))]
        public async Task<IActionResult> GetByUrl([FromQuery] GetByIdRequestDto model)
            => Ok(await _client.GetTrackByIdAsync(model));

        [HttpGet("Url")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongResponseDto))]
        public async Task<IActionResult> GetByUrl([FromQuery] GetByUrlRequestDto model)
            => Ok(await _client.GetTrackByUrlAsync(model));

        [HttpGet("Isrc")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongResponseDto))]
        public async Task<IActionResult> GetByIsrc([FromQuery] GetByIsrcRequestDto model)
            => Ok(await _client.GetTrackByIsrcAsync(model));

        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongsResponseDto))]
        public async Task<IActionResult> Find([FromQuery] FindSongsRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}