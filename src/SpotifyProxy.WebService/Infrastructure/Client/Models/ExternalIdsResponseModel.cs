using System;
using Newtonsoft.Json;

namespace SpotifyProxy.WebService.Infrastructure.Client.Models
{
    [JsonObject]
    public class ExternalIdsResponseModel
	{
        [JsonProperty("upc")]
        public string? Upc { get; set; }

        [JsonProperty("isrc")]
        public string Isrc { get; set; }
    }
}