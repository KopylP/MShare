﻿using Newtonsoft.Json;

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
        public AlbumResponseModel Album { get; set; }

        [JsonProperty("artists")]
        public ArtistResponseModel[] Artists { get; set; }
    }
}

