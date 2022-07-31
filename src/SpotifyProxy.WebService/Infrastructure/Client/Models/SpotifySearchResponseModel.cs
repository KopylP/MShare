using System;
using System.Collections;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    internal class SpotifySearchResponseModel
    {
        [JsonProperty("tracks")]
        public TrackList Tracks { get; set; }

        [JsonObject]
        public class TrackList
        {
            [JsonProperty("items")]
            public SpotifyTrackResponseModel[] Items { get; set; }
        }
    }
}

