using MShare.Framework.Application.Context;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
	public class QueryContext : IRequestContext<GetSongByUrlQuery, SongResponseDto>
    {
        public ProxyService.Client.SongResponseDto? ServiceProxyResponse { get; set; }
        public StreamingServiceType ServiceType { get; set; }
    }
}