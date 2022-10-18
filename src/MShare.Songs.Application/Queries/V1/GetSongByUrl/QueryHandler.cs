using AutoMapper;
using MediatR;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.Application.SqlClient;
using MShare.Framework.Types.Addresses;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Queries.V1.GetSongByUrl
{
    internal class QueryHandler : IQueryHandler<GetSongByUrlQuery, SongResponseDto>
    {
        private readonly IStreamingServiceTypeRecognizer _recognizer;
        private readonly IProxyServiceClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly QueryContext _context;
        private readonly ISqlQueryExecutor _sqlQueryExecutor;
        private readonly IMetadataExtractor _idExtractor;
        private readonly IExecutionContext _executionContext;

        public QueryHandler(
            IRequestContext<GetSongByUrlQuery, SongResponseDto> context,
            IStreamingServiceTypeRecognizer recognizer,
            IProxyServiceClientFactory clientFactory,
            IMapper mapper,
            ISqlQueryExecutor sqlQueryExecutor,
            IMetadataExtractor idExtractor,
            IExecutionContext executionContext)
        {
            _recognizer = recognizer;
            _clientFactory = clientFactory;
            _mapper = mapper;
            _context = (QueryContext)context;
            _sqlQueryExecutor = sqlQueryExecutor;
            _idExtractor = idExtractor;
            _executionContext = executionContext;
        }

        public async Task<SongResponseDto> Handle(GetSongByUrlQuery request, CancellationToken cancellationToken)
        {
            var recognizerServiceResult = _recognizer.From(new Uri(request.SongUrl));
            BadRequestException.ThrowIf(recognizerServiceResult.IsFail, recognizerServiceResult.FailMessage);

            var song = await GetSongFromDatabase(recognizerServiceResult.Data, request.SongUrl);

            if (song is null)
            {

                var serviceClient = _clientFactory.Create(recognizerServiceResult.Data);

                var songResult = await serviceClient.GetSongByUrlAsync(request.SongUrl);
                NotFoundException.ThrowIf(songResult is null, "Song not found");

                _context.ServiceProxyResponse = songResult;
                _context.ServiceType = recognizerServiceResult.Data;

                song = _mapper.Map<SongResponseDto>((songResult, recognizerServiceResult.Data));
            }

            return song;
        }

        public async Task<SongResponseDto?> GetSongFromDatabase(StreamingServiceType streamingService, string url)
        {
            var idResult = _idExtractor.ExtractId(url, streamingService, MediaType.Song);
            var regionResult = _idExtractor.ExtractRegion(url, streamingService, MediaType.Song);

            if (idResult.IsSuccess)
            {
                var sql =
                    $"SELECT " +
                        $"service_type as ServiceType, source_id SongSourceId, " +
                        $"image_url CoverImageUrl, source_url SongUrl, " +
                        $"artist_name ArtistName, album_name AlbumName," +
                        $"name SongName " +
                    $"FROM song " +
                        $"WHERE service_type = '{streamingService}' " +
                        $"AND source_id='{idResult.Data}' " +
                        $"AND region='{regionResult.Data}'";

                var songs = await _sqlQueryExecutor.QueryAsync<SongResponseDto>(sql);
            }

            return null;
        }
    }
}