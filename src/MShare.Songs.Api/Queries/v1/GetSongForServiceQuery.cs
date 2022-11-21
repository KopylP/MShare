using System;
using MShare.Framework.Api;
using MShare.Framework.Api.Shared;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Api.V1.Queries
{
	public class GetSongForServiceQuery : IQuery<ListResponseDto<SongResponseDto>>
	{
		public string OriginService { get; set; }
		public string SourceId { get; set; }
		public string DestinationService { get; set; }
	}
}

