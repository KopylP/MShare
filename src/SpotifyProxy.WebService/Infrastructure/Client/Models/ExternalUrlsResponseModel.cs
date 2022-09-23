using System;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    internal class ExternalUrlsResponseModel
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}

