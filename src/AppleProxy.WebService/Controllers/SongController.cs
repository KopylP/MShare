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
        private const int MAX_LIMIT = 5;

        private readonly IStreamingServiceClient _client;

        public SongController(IStreamingServiceClient client) => _client = client;

        [HttpGet("Url")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongResponseDto))]
        public async Task<IActionResult> GetByUrl([FromQuery] GetSongByUrlRequestDto model)
            => Ok(await _client.GetByUrlAsync(model));

        [HttpGet("Find")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongsResponseDto))]
        public async Task<IActionResult> Find([FromQuery] FindSongsRequestDto model)
        {
            var response = await _client.FindAsync(model with { AlbumName = string.Empty });

            if (response.IsEmpty)
            {
                response = await _client.FindAsync(model with { ArtistName = string.Empty }, limit: 10);
                FilterItemsByArtistName(response, model);
            }

            if (response.IsEmpty)
            {
                response = await _client.FindAsync(model with { AlbumName = string.Empty, ArtistName = string.Empty }, limit: 10);
                FilterItemsByArtistName(response, model);
            }

            if (response.IsEmpty)
                throw new NotFoundException();

            return Ok(response);
        }

        private void FilterItemsByArtistName(SongsResponseDto response, FindSongsRequestDto request)
        {
            if (!string.IsNullOrWhiteSpace(request.ArtistName) && response is not null)
            {
                var lengthRange = (
                    Min: request.ArtistName.Unidecode().Length - 1,
                    Max: request.ArtistName.Unidecode().Length + 1);

                response.Items = response.Items.Where(item =>
                {
                    var artists = item.Artists.First().Name.Split("&").Select(p => p.Unidecode());
                    return artists.Any(artist => artist.Length >= lengthRange.Min && artist.Length <= lengthRange.Max);
                })
                .Take(MAX_LIMIT)
                .ToArray();
            }
        }
    }
}