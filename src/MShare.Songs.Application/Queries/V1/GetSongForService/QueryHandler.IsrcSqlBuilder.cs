﻿using System;
using MShare.Songs.Abstractions;
using MShare.Songs.Application.Queries.V1.Shared.SqlBuilders;

namespace MShare.Songs.Application.Queries.V1.GetSongForService
{
	internal partial class QueryHandler
	{
		private class SqlBuilder : SongSqlBuilderBase
		{
			private readonly string _region;
			private readonly string _streamingService;
			private readonly string _sourceId;

			private SqlBuilder(string region, string streamingService, string sourceId)
			{
				_streamingService = streamingService;
				_sourceId = sourceId;
				_region = region;
			}

			public static SqlBuilder Of(string region, string streamingService, string sourceId)
				=> new SqlBuilder(region, streamingService, sourceId);

            protected override void ApplySelect(SelectBuilder selectBuilder)
            {
                selectBuilder.Select("isrc Isrc");
            }

            protected override void ApplyFilters(FilterBuilder filterBuilder)
            {
				filterBuilder.Where($"service_type = '{_streamingService}'");
				filterBuilder.Where($"source_id='{_sourceId}'");
				filterBuilder.Where($"(region='{_region}' OR 1 = 1)");
            }
        }
	}
}

