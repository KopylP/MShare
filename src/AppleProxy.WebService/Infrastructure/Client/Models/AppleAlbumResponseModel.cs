using System;
namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleAlbumResponseModel
	{
        // album
        public string CollectionName { get; set; }
        public string CollectionId { get; set; }
        public string ArtworkUrl100 { get; set; }
        public string CollectionViewUrl { get; set; }
        public string Country { get; set; }
        // artist
        public string ArtistName { get; set; }
        public string ArtistId { get; set; }
    }
}

