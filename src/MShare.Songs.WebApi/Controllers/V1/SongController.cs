﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Api;
using MShare.Framework.WebApi.Filters;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Api.V1.Queries.Dtos;

namespace MShare.Songs.WebApi.Controllers.V1
{
    public class SongController : ControllerBase
    {
        public static IContextExecutor _executor;

        public SongController(IContextExecutor executor): base(executor) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SongByUrlResponseDto))]
        public async Task<IActionResult> Get([FromQuery] string url) => await ExecuteAsync(GetSongByUrlQuery.Of(url));
    }
}
