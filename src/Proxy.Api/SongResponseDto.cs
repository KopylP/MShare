using System;
namespace Proxy.Api
{
    public record SongResponseDto
    {
        public SongSourceDto Song { get; set; }
        public AlbumSourceDto Album { get; set; }
        public ArtistSourceDto[] Artists { get; set; }
    }
}

