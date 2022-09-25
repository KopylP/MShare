using System;
using System.Text.Json.Serialization;

namespace MShare.Songs.Api.Queries.Dtos.V1
{
	public class GetByUrlResponseDto
	{
		public string MediaType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public SongResponseDto Song { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public AlbumResponseDto Album { get; set; }

		public ServicesResponseDto.ItemDto[] Services { get; set; }
	}
}