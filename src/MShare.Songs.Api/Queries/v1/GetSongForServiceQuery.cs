using System;
using MShare.Framework.Api;
using MShare.Framework.Api.Shared;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Api.V1.Queries
{
	public class GetSongForServiceQuery : IQuery<ListResponseDto<SongResponseDto>>
	{
		public StreamingServiceType OriginService { get; set; }
		public string SourceId { get; set; }
		public StreamingServiceType DestinationService { get; set; }
	}
}

