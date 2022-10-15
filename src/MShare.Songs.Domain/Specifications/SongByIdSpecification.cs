using System;
using MShare.Framework.Domain;
using MShare.Songs.Abstractions;
using System.Linq.Expressions;

namespace MShare.Songs.Domain.Specifications
{
	public class SongByIdSpecification : SpecificationBase<SongEntity>
    {
        private readonly string _sourceId;
        private readonly StreamingServiceType _streamingType;

        private SongByIdSpecification(string sourceId, StreamingServiceType serviceType)
        {
            _sourceId = sourceId;
            _streamingType = serviceType;
        }

        public override Expression<Func<SongEntity, bool>> Criteria =>
            p => p.SourceId == _sourceId && p.ServiceType == _streamingType;

        public static SongByIdSpecification Of(string sourceId, StreamingServiceType serviceType)
            => new SongByIdSpecification(sourceId, serviceType);
    }
}

