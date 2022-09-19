using System;
namespace Proxy.Api
{
    public record SongSourceDto
    {
        public string Name { get; init; }
        public string SourceId { get; init; }
        public string SourceUrl { get; init; }
        public string Country { get; init; }

        public static SongSourceDto Of(string name, string sourceId, string sourceUrl, string? country = default)
            => new SongSourceDto
            {
                Name = name,
                SourceId = sourceId,
                SourceUrl = sourceUrl,
                Country = country ?? Region.Invariant
            };
    }
}

