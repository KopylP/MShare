using System;
namespace Proxy.Api
{
    public record SongSourceDto
    {
        public string Name { get; init; }
        public string SourceId { get; init; }
        public string SourceUrl { get; init; }
        public string Region { get; init; }
        public string Isrc { get; init; }

        public static SongSourceDto Of(string name, string sourceId, string sourceUrl, string isrc, string? region = default)
            => new SongSourceDto
            {
                Name = name,
                SourceId = sourceId,
                SourceUrl = sourceUrl,
                Isrc = isrc,
                Region = region ?? "Invariant"
            };
    }
}

