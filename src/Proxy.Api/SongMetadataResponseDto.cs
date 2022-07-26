using System;
namespace Proxy.Api
{
    public class SongMetadataResponseDto
    {
        public MetadataDto Song { get; init; }
        public MetadataDto Album { get; init; }
        public MetadataDto[] Artists { get; init; }
    }
}

