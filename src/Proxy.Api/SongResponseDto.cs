using System;
namespace Proxy.Api
{
    public record SongResponseDto
    {
        public SongSourceDto Song { get; init; }
        public AlbumSourceDto Album { get; init; }
        public ArtistSourceDto[] Artists { get; init; }
    }
}

