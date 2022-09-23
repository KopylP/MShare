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

            FilterItemsByArtistName(response, request);
            FilterItemsBySongName(response, request);

            response.Items = response.Items.Take(take).ToArray();

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
                .ToArray();
            }
        }

        private void FilterItemsBySongName(SongsResponseDto response, FindSongsRequestDto request)
        {
            if (!string.IsNullOrWhiteSpace(request.SongName) && response is not null)
            {
                var items = response
                    .Items
                    .Where(item => item.Song.Name.ToLower() == request.SongName.ToLower())
                    .ToArray();

                if (items.Any())
                {
                    response.Items = items;
                }
            }
        }
    }
}