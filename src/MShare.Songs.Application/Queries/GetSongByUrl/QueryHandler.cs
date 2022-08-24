using MShare.Framework.Application;
using MShare.Songs.Api.Queries;
using MShare.Songs.Api.Queries.Dtos;

namespace MShare.Songs.Application.Queries.GetSongByUrl
{
    internal class QueryHandler : IQueryHandler<GetSongByUrlQuery, SongByUrlResponseDto>
    {
        public Task<SongByUrlResponseDto> Handle(GetSongByUrlQuery request, CancellationToken cancellationToken)
        {
        }
    }
}