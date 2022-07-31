using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    internal class SpotifyTrackResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("album")]
        public AlbumModel Album { get; set; }

        [JsonProperty("artists")]
        public ArtistModel[] Artists { get; set; }
    }

    [JsonObject]
    internal class AlbumModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        [JsonProperty("images")]
        public ImageModel[] Images { get; set; }
    }

    [JsonObject]
    internal class ArtistModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

    }

    [JsonObject]
    internal class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    [JsonObject]
    public class ImageModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}

