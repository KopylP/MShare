using MShare.Framework.Api;
using MShare.Songs.Api.Queries.Dtos.V1;

namespace MShare.Songs.Api.V1.Queries
{
	public class GetAlbumByUrlQuery : IQuery<AlbumResponseDto>
	{
        public string AlbumUrl { get; set; }

        public static GetAlbumByUrlQuery Of(string url)
            => new GetAlbumByUrlQuery
            {
                AlbumUrl = url
            };
    }
}