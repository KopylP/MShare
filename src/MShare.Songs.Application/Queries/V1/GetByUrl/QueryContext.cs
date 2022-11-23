using System;
using MShare.Framework.Application.Context;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;

namespace MShare.Songs.Application.Queries.V1.GetByUrl
{
	public class QueryContext : IQueryContext<GetByUrlQuery, GetByUrlResponseDto>
	{
		public MediaType MediaType { get; set; }
		public StreamingServiceType ServiceType { get; set; }
	}
}

