using System;
using MShare.Framework.Domain;
using MShare.Framework.Domain.Exceptions;
using MShare.Framework.Exceptions;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain.Specifications;

namespace MShare.Songs.Domain
{
	public class AlbumEntity : EntityBase, IEntity
	{
		public virtual string Name { get; init; }
		public virtual string ArtistName { get; init; }
		public virtual StreamingServiceType ServiceType { get; protected set; }
        public string ImageUrl { get; init; }
        public string ImageThumbnailUrl { get; init; }
        public string SourceId { get; protected set; }
        public string SourceUrl { get; init; }
        public CountryCode3 Country { get; protected set; }

        protected AlbumEntity()
        {
        }

        public AlbumEntity(string sourceId, StreamingServiceType serviceType, CountryCode3 country)
        {
            Thrower.ThrowIf<ArgumentException>(string.IsNullOrEmpty(sourceId), "Source Id is null");
            Thrower.ThrowIf<ArgumentException>(country is null, "Country is null");

            SourceId = sourceId;
            ServiceType = serviceType;
            Country = country;
        }

        protected override void OnSaving()
        {
            Check(AlbumIsValidSpecification.Instance.IsSatisfiedBy(this), "Abmum entity is invalid");
        }
    }
}