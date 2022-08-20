﻿namespace AppleProxy.WebService.Infrastructure.Client.Models
{
	public class AppleTrackResponseModel
	{
		// track
		public string TrackName { get; set; }
		public string TrackId { get; set; }
		public string TrackViewUrl { get; set; }
		// album
		public string CollectionName { get; set; }
		public string CollectionId { get; set; }
		public string ArtworkUrl100 { get; set; }
		// artist
		public string ArtistName { get; set; }
		public string ArtistId { get; set; }
    }
}

