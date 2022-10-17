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
        public ExternalUrlsResponseModel ExternalUrls { get; set; }

        [JsonProperty("album")]
        public SpotifyAlbumResponseModel Album { get; set; }

        [JsonProperty("artists")]
        public SpotifyArtistResponseModel[] Artists { get; set; }

        [JsonProperty("external_ids")]
        public ExternalIdsResponseModel ExternalIds { get; set; }
    }
}

