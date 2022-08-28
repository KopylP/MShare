using System;
namespace MShare.Songs.Api.V1.Queries.Dtos
{
	public class SongByUrlResponseDto
	{
		public string SongUrl { get; set; }
		public string SongName { get; set; }
		public string ArtistName { get; set; }
		public string AlbumName { get; set; }
		public string CoverImageUrl { get; set; }

		public ServiceDto[] Services { get; set; }

		public class ServiceDto
		{
			public string Name { get; set; }
			public string Type { get; set; }
			public bool IsAvailable { get; set; }
		}
	}
}

