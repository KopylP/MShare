using System;
using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos;

namespace MShare.Songs.Api.Queries
{
	public class GetSongByUrlQuery : IQuery<SongByUrlResponseDto>
	{
		public string SongUrl { get; set; }
	}
}

