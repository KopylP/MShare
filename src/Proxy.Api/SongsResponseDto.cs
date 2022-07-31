using System.Text.Json.Serialization;

namespace Proxy.Api
{
    public record SongsResponseDto
    {
        public SongResponseDto[] Items { get; set; }

        [JsonIgnore]
        public bool IsEmpty => !Items?.Any() ?? true;
    }
}

