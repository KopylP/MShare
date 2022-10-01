using System;
using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Api.V1.Queries
{
	public record GetSongByUrlQuery : IQuery<SongResponseDto>
	{
		public string SongUrl { get; set; }

        public static GetSongByUrlQuery Of(string url)
            => new GetSongByUrlQuery
            {
                SongUrl = url
            };
    }
}

