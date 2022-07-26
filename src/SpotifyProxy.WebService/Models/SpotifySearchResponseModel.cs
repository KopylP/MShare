using System;
using System.Collections;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Models
{
    [JsonObject]
    public class SpotifySearchResponseModel : IEnumerable<SpotifyTrackResponseModel>
    {
        [JsonProperty("tracks")]
        public TrackList Tracks { get; set; }

        public IEnumerator<SpotifyTrackResponseModel> GetEnumerator() => Tracks.Items.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [JsonObject]
        public class TrackList
        {
            [JsonProperty("items")]
            public SpotifyTrackResponseModel[] Items { get; set; }
        }
    }
}

