using MShare.Framework.Domain;
using MShare.Framework.Exceptions;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Domain
{
	public class AlbumEntity : EntityBase, IEntity
	{
        public long Id { get; protected set; }
        public DateTime CreationDate { get; set; }
		public string Name { get; init; }
		public string ArtistName { get; init; }
		public StreamingServiceType ServiceType { get; protected set; }
        public string ImageUrl { get; init; }
        public string ImageThumbnailUrl { get; init; }
        public string SourceId { get; protected set; }
        public string SourceUrl { get; init; }
        public CountryCode2 Region { get; protected set; }
        public Upc Upc { get; init; }

        protected AlbumEntity()
        {
        }

        public AlbumEntity(string sourceId, StreamingServiceType serviceType, CountryCode2 region, Upc upc)
        {
            Thrower.ThrowIf<ArgumentException>(string.IsNullOrEmpty(sourceId), "Source Id is null");
            Thrower.ThrowIf<ArgumentException>(region is null, "Country is null");

            SourceId = sourceId;
            ServiceType = serviceType;
            Region = region;
            CreationDate = DateTime.UtcNow;
            Upc = upc;
        }

        protected override void OnSaving()
        {
            Check(AlbumIsValidSpecification.Instance.IsSatisfiedBy(this), "Abmum entity is invalid");
        }
    }
}