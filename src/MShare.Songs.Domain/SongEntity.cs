using MShare.Framework.Domain;
using MShare.Framework.Exceptions;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Domain
{
    public class SongEntity : EntityBase, IEntity
    {
        public virtual DateTime CreationDate { get; set; }
        public virtual string Name { get; init; }
        public virtual string ArtistName { get; init; }
        public virtual string AlbumName { get; init; }
        public virtual string AlbumSourceId { get; init; }
        public virtual StreamingServiceType ServiceType { get; protected set; }
        public string ImageUrl { get; init; }
        public string ImageThumbnailUrl { get; init; }
        public string SourceId { get; protected set; }
        public string SourceUrl { get; init; }
        public CountryCode2 Region { get; protected set; }
        public Isrc Isrc { get; init; }

        protected SongEntity()
		{
		}

        public SongEntity(string sourceId, StreamingServiceType serviceType, CountryCode2 region, Isrc isrc)
        {
            Thrower.ThrowIf<ArgumentException>(string.IsNullOrEmpty(sourceId), "Source Id is null");
            Thrower.ThrowIf<ArgumentException>(region is null, "Country is null");

            SourceId = sourceId;
            ServiceType = serviceType;
            Region = region;
            CreationDate = DateTime.UtcNow;
            Isrc = isrc;
        }

        protected override void OnSaving()
        {
            Check(SongIsValidSpecification.Instance.IsSatisfiedBy(this), "Song entity is invalid");
        }
    }
}

