using System;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Application.Queries.V1.Shared.QueryContexts
{
	public interface ISongResponseQueryContext
	{
        public ProxyService.Client.SongResponseDto? ServiceProxyResponse { get; }
        public StreamingServiceType ServiceType { get; }
    }
}

