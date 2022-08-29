using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Api;
using MShare.Framework.WebApi.Filters;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class SongController : ControllerBase
    {
        public static IContextExecutor _executor;

        public SongController(IContextExecutor executor): base(executor) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongByUrlResponseDto))]
        public async Task<IActionResult> Get([FromQuery] [Required] [Url] string url) => await ExecuteAsync(GetSongByUrlQuery.Of(url));
    }
}