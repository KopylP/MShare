namespace Proxy.Api
{
    public record AlbumSourceDto
    {
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string ImageThumbnailUrl { get; init; }
        public string SourceId { get; init; }
        public string SourceUrl { get; init; }
        public string Country { get; init; }
    }
}

