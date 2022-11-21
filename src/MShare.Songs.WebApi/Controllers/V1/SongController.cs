using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Api;
using MShare.Framework.Api.Shared;
using MShare.Framework.WebApi;
using MShare.Framework.WebApi.Filters;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class SongController : ControllerBase
    {
        public static IHttpContextExecutor _executor;

        public SongController(IHttpContextExecutor executor): base(executor) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiError))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetByUrlResponseDto))]
        public async Task<IActionResult> Get([FromQuery] [Required] [Url] string url) => await ExecuteAsync(GetByUrlQuery.Of(url));

        //public string OriginSerivice { get; set; }
        //public string SourceId { get; set; }
        //public string DestinationService { get; set; }
        [HttpGet("{originService}/{sourceId}/for/{destinationService}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiError))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ListResponseDto<SongResponseDto>))]
        public async Task<IActionResult> GetForService(string originService, string sourceId, string destinationService)
            => await ExecuteAsync(new GetSongForServiceQuery
            {
                DestinationService = destinationService,
                OriginService = originService,
                SourceId = sourceId
            });
    }
}