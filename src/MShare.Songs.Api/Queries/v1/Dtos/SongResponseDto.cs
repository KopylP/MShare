using System;
namespace MShare.Songs.Api.Queries.Dtos.V1
{
	public record SongResponseDto
	{
		public string SongSourceId { get; set; }
		public string SongUrl { get; set; }
		public string SongName { get; set; }
		public string ArtistName { get; set; }
		public string AlbumName { get; set; }
		public string CoverImageUrl { get; set; }
		public string ServiceType { get; set; }
	}
}

