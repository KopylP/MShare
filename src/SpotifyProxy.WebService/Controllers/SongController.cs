using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Exceptions;
using MShare.Framework.WebApi.Filters;
using Proxy.Api;

namespace SpotifyProxy.WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExceptionFilter]
public class SongController : ControllerBase
{
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
            response = await _client.FindAsync(model with { ArtistName = string.Empty, SongName = $"{model.SongName} {model.ArtistName}" });

        if (response.IsEmpty)
            response = await _client.FindAsync(model with { AlbumName = string.Empty, ArtistName = string.Empty });

        if (response.IsEmpty)
            throw new NotFoundException();

        return Ok(response);
    }
}

