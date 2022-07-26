using System;
using System.Collections;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Models
{
    [JsonObject]
    public class SpotifyTrackResponseModel : MetadataModel, IEnumerable<SpotifyTrackResponseModel>
    {
        [JsonProperty("album")]
        public MetadataModel Album { get; set; }

        [JsonProperty("artists")]
        public MetadataModel[] Artists { get; set; }

        public IEnumerator<SpotifyTrackResponseModel> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    [JsonObject]
    public class MetadataModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

    }

    [JsonObject]
    public class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }
}

