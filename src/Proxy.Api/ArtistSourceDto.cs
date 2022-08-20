namespace Proxy.Api
{
    public class ArtistSourceDto
    {
        public string Name { get; init; }
        public string SourceId { get; init; } 

        public static ArtistSourceDto Of(string name, string sourceId)
            => new ArtistSourceDto { Name = name, SourceId = sourceId };
    }
}