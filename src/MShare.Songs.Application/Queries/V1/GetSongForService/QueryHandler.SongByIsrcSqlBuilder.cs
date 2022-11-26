using System;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Application.Queries.V1.Shared.SqlBuilders;

namespace MShare.Songs.Application.Queries.V1.GetSongForService
{
	internal partial class QueryHandler
	{
		private class SongByIsrcSqlBuilder : SongSqlBuilderBase
		{
            private readonly string _region;
            private readonly string _streamingService;
            private readonly string _isrc;

            private SongByIsrcSqlBuilder(string region, string streamingService, string isrc)
            {
                _streamingService = streamingService;
                _isrc = isrc;
                _region = region.ToUpperInvariant();
            }

            public static SongByIsrcSqlBuilder Of(string region, StreamingServiceType streamingService, string isrc)
                => new SongByIsrcSqlBuilder(region, streamingService.ToString(), isrc);

            protected override void ApplyFilters(FilterBuilder filterBuilder)
            {
                base.ApplyFilters(filterBuilder);

                filterBuilder.Where($"service_type = '{_streamingService}'");
                filterBuilder.Where($"isrc='{_isrc}'");
                filterBuilder.WhereIf(_region != CountryCode2.Invariant, $"(region = '{CountryCode2.Invariant}' OR region='{_region}')");
            }
        }
	}
}

