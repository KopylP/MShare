using System;
using MShare.Framework.Api;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Api.Messages
{
    public record UnsavedAlbumRequestedEvent : IIntegrationEvent
    {
        public string Name { get;  set; }
        public string ArtistName { get; set; }
        public StreamingServiceType ServiceType { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public string SourceId { get; set; }
        public string SourceUrl { get; set; }
        public string Region { get; set; }
        public string Upc { get; set; }
    }
}