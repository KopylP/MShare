using System;
using MShare.Framework.Api;

namespace MShare.Songs.Api.Messages
{
    public record SongRequestedEvent : IIntegrationEvent
    {
        public Guid CommandId { get; set; }

        public DateTime Timestamp => DateTime.Now;
    }
}