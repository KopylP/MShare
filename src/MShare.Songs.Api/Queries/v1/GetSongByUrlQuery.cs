using System;
using MShare.Framework.Api;
using MShare.Songs.Api.V1.Queries.Dtos;

namespace MShare.Songs.Api.V1.Queries
{
	public class GetSongByUrlQuery : IQuery<SongByUrlResponseDto>
	{
		public string SongUrl { get; set; }

        public static GetSongByUrlQuery Of(string url)
            => new GetSongByUrlQuery
            {
                SongUrl = url
            };
    }
}

