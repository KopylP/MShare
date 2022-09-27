using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Exceptions;
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

        [HttpGet("Url")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongResponseDto))]
        public async Task<IActionResult> GetByUrl([FromQuery] GetByUrlRequestDto model)
            => Ok(await _client.GetSongByUrlAsync(model));

        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongsResponseDto))]
        public async Task<IActionResult> Find([FromQuery] FindSongsRequestDto request)
        {
            int take = 1;

            var response = await _client.FindSongAsync(request with { AlbumName = string.Empty }, limit: MAX_LIMIT);

            if (response.IsEmpty)
            {
                take = MAX_TAKE;
                response = await _client.FindSongAsync(request with { ArtistName = string.Empty }, limit: MAX_LIMIT);
            }

            if (response.IsEmpty)
            {
                take = MAX_TAKE;
                response = await _client.FindSongAsync(request with { AlbumName = string.Empty, ArtistName = string.Empty }, limit: MAX_LIMIT);
            }

            if (response.IsEmpty)
                throw new NotFoundException();

            response.FilterItemsByArtistName(request.ArtistName);
            response.FilterItemsBySongName(request.SongName);

            response.Items = response.Items.Take(take).ToArray();

            return Ok(response);
        }
    }
}