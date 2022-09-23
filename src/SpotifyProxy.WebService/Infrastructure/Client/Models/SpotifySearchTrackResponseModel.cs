using System;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    internal class SpotifySearchTrackResponseModel
    {
        [JsonProperty("tracks")]
        internal TrackList Tracks { get; set; }

        [JsonObject]
        internal class TrackList
        {
            [JsonProperty("items")]
            public SpotifyTrackResponseModel[] Items { get; set; }
        }
    }
}

