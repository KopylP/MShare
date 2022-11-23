using System;
using MediatR;
using MShare.Framework.Application.Context;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.Application.Queries.V1.GetAlbumByUrl
{
	public class QueryContext : IQueryContext<GetAlbumByUrlQuery, AlbumResponseDto>
    {
        public ProxyService.Client.AlbumResponseDto? ServiceProxyResponse { get; set; }
        public StreamingServiceType ServiceType { get; set; }
    }
}