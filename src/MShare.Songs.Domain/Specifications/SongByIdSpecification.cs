using System;
using MShare.Framework.Domain;
using MShare.Songs.Abstractions;
using System.Linq.Expressions;
using MShare.Framework.Types.Addresses;

namespace MShare.Songs.Domain.Specifications
{
	public class SongByIdSpecification : SpecificationBase<SongEntity>
    {
        private readonly string _sourceId;
        private readonly StreamingServiceType _streamingType;
        private readonly CountryCode2 _region;

        private SongByIdSpecification(string sourceId, StreamingServiceType serviceType, CountryCode2 region)
        {
            _sourceId = sourceId;
            _streamingType = serviceType;
            _region = region;
        }

        public override Expression<Func<SongEntity, bool>> Criteria =>
            p => p.SourceId == _sourceId && p.ServiceType == _streamingType && p.Region == _region;

        public static SongByIdSpecification Of(string sourceId, StreamingServiceType serviceType, string region)
            => new SongByIdSpecification(sourceId, serviceType, region);
    }
}

