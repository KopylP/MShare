using System;
namespace Proxy.Api
{
    public class MetadataDto
    {
        public string Name { get; init; }
        public string SourceId { get; init; }
        public string SourceUrl { get; init; }

        public static MetadataDto Of(string name, string sourceId, string sourceUrl)
            => new MetadataDto
            {
                Name = name,
                SourceId = sourceId,
                SourceUrl = sourceUrl
            };
    }
}

