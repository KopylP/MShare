using System;
namespace Proxy.Api
{
    public record SongSourceDto
    {
        public string Name { get; init; }
        public string SourceId { get; init; }
        public string SourceUrl { get; init; }

        public static SongSourceDto Of(string name, string sourceId, string sourceUrl)
            => new SongSourceDto
            {
                Name = name,
                SourceId = sourceId,
                SourceUrl = sourceUrl
            };
    }
}

