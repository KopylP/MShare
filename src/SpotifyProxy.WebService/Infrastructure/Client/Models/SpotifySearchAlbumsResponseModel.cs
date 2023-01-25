using System;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    public class SpotifySearchAlbumsResponseModel
	{
        [JsonProperty("albums")]
        internal TrackList Albums { get; set; }

        [JsonObject]
        internal class TrackList
        {
            [JsonProperty("items")]
            public ItemDto[] Items { get; set; }
        }

        [JsonObject]
        public class ItemDto
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }
    }
}

