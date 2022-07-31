namespace Proxy.Api
{
    public record AlbumSourceDto
    {
        public string Name { get; init; }
        public string ImageUrl { get; init; }

        public static AlbumSourceDto Of(string name, string imageUrl) => new AlbumSourceDto
        {
            ImageUrl = imageUrl,
            Name = name
        };
    }
}

