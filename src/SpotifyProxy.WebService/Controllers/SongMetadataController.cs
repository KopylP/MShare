using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Infrastructure.Clients;
using MShare.Framework.Types;
using MShare.Framework.WebApi.Exceptions;
using Proxy.Api;
using SpotifyProxy.WebService.Helpers;
using SpotifyProxy.WebService.Infrastructure;
using SpotifyProxy.WebService.Models;

namespace SpotifyProxy.WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SongMetadataController : ControllerBase
{
    private readonly ILogger<SongMetadataController> _logger;
    private readonly string _publicApiUrl;
    private readonly IServiceProvider _provider;

    public SongMetadataController(ILogger<SongMetadataController> logger, IConfiguration configuration, IServiceProvider provider)
    {
        _logger = logger;
        _publicApiUrl = configuration.GetValue<string>("SpotifyPublicApiUrl");
        _provider = provider;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongMetadataResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] SongMetadataRequestDto model)
    {
        try
        {
            var response = await SearchTrack(model);

            if (!response.Any())
                return NotFound();

            var track = response.First();
            return Ok(Map(track));
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }

    }

    private async Task<IEnumerable<SpotifyTrackResponseModel>> SearchTrack(SongMetadataRequestDto model)
        => model.IsSearchByName() switch
        {
            true => await _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "track")
                .SetQueryParam("q", $"track: {model.SongName} +artist:{model.ArtistName}")
                .SetQueryParam("limit", 1)
                .GetAuthorizedAsync<SpotifySearchResponseModel>(_provider),

            false => await _publicApiUrl
                .AppendPathSegment("tracks")
                .AppendPathSegment(model.Url?.GetSpotifySongId())
                .GetAuthorizedAsync<SpotifyTrackResponseModel>(_provider)
        };

    private SongMetadataResponseDto Map(SpotifyTrackResponseModel model)
    {
        return new SongMetadataResponseDto
        {
            Song = MetadataDto.Of(model.Name, model.Id, model.ExternalUrls.Spotify),
            Album = MetadataDto.Of(model.Album.Name, model.Album.Id, model.Album.ExternalUrls.Spotify),
            Artists = model.Artists.Select(p => MetadataDto.Of(p.Name, p.Id, p.ExternalUrls.Spotify)).ToArray()
        };
    }
}

