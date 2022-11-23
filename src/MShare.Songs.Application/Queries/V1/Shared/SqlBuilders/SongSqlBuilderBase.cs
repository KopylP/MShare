using System;
using Dapper;
using MShare.Framework.Application.SqlClient;

namespace MShare.Songs.Application.Queries.V1.Shared.SqlBuilders
{
	public class SongSqlBuilderBase : SqlBuilderBase
    {
        protected override void ApplySelect(SelectBuilder selectBuilder)
        {
            selectBuilder.Select("service_type as ServiceType");
            selectBuilder.Select("source_id SongSourceId");
            selectBuilder.Select("image_url CoverImageUrl");
            selectBuilder.Select("source_url SongUrl");
            selectBuilder.Select("artist_name ArtistName");
            selectBuilder.Select("album_name AlbumName");
            selectBuilder.Select("name SongName");
        }

        protected override sealed string GetTamplate()
        {
            return "SELECT /**select**/ FROM song /**where**/";
        }
    }
}