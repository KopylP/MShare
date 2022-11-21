using System;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Application.Queries.V1.Shared.SqlBuilders;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
	internal partial class QueryHandler
	{
		private class SqlBuilder : SongSqlBuilderBase
		{
			private readonly StreamingServiceType _streamingServiceType;
			private readonly string _id;
			private readonly CountryCode2 _region;

			private SqlBuilder(StreamingServiceType streamingServiceType, string id, CountryCode2 region)
			{
				_streamingServiceType = streamingServiceType;
				_id = id;
				_region = region;
			}

			public static SqlBuilder Of(StreamingServiceType streamingServiceType, string id, CountryCode2 region)
				=> new SqlBuilder(streamingServiceType, id, region);

            protected override void ApplyFilters(FilterBuilder filterBuilder)
            {
				filterBuilder.Where($"service_type = '{_streamingServiceType}'");
				filterBuilder.Where($"source_id='{_id}'");
				filterBuilder.Where($"region='{_region}'");
            }
        }
	}
}

