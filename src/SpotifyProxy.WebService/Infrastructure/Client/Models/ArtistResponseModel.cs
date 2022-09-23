using System;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    internal class ArtistResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrlsResponseModel ExternalUrls { get; set; }

    }
}

