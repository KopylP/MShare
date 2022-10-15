using System.Linq.Expressions;
using MShare.Framework.Domain;
using MShare.Songs.Abstractions;

namespace MShare.Songs.Domain.Specifications
{
	public class AlbumByIdSpecification : SpecificationBase<AlbumEntity>
	{
		private readonly string _sourceId;
		private readonly StreamingServiceType _streamingType;

		private AlbumByIdSpecification(string sourceId, StreamingServiceType serviceType)
		{
			_sourceId = sourceId;
			_streamingType = serviceType;
		}

		public override Expression<Func<AlbumEntity, bool>> Criteria =>
			p => p.SourceId == _sourceId && p.ServiceType == _streamingType;

        public static AlbumByIdSpecification Of(string sourceId, StreamingServiceType serviceType)
			=> new AlbumByIdSpecification(sourceId, serviceType);
    }
}

