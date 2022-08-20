namespace Proxy.Api
{
    public record AlbumSourceDto
    {
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        public string ImageThumbnailUrl { get; init; }
        public string SourceId { get; init; }

        public static AlbumSourceDto Of(string name, string sourceId, string imageUrl, string imageThumbnailUrl) => new AlbumSourceDto
        {
            ImageUrl = imageUrl,
            Name = name,
            ImageThumbnailUrl = imageThumbnailUrl,
            SourceId = sourceId
        };
    }
}

