using System;
namespace MShare.Songs.Api.Queries.Dtos.V1
{
	public record AlbumResponseDto
	{
        public string AlbumSourceId { get; set; }
        public string AlbumUrl { get; set; }
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public string CoverImageUrl { get; set; }
        public string ServiceType { get; set; }
    }
}

