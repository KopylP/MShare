using System.Linq.Expressions;
using MShare.Framework.Domain;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain.Specifications
{
	public class AlbumByIdSpecification : SpecificationBase<AlbumEntity>
	{
		private readonly string _sourceId;
		private readonly StreamingServiceType _streamingType;
		private readonly CountryCode2 _region;

		private AlbumByIdSpecification(string sourceId, StreamingServiceType serviceType, CountryCode2 region)
		{
			_sourceId = sourceId;
			_streamingType = serviceType;
			_region = region;
		}

		public override Expression<Func<AlbumEntity, bool>> Criteria =>
			p => p.SourceId == _sourceId && p.ServiceType == _streamingType && p.Region == _region;

        public static AlbumByIdSpecification Of(string sourceId, StreamingServiceType serviceType, string region)
			=> new AlbumByIdSpecification(sourceId, serviceType, region);
    }
}

