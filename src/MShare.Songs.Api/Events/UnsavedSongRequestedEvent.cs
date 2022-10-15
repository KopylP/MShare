using System;
using MShare.Framework.Api;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Api.Events
{
	public record UnsavedSongRequestedEvent : IIntegrationEvent
    {
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string AlbumSourceId { get; set; }
        public string AlbumSourceUrl { get; set; }
        public StreamingServiceType ServiceType { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string SourceId { get; set; }
        public string SourceUrl { get; set; }
        public string Country { get; set; }
    }
}