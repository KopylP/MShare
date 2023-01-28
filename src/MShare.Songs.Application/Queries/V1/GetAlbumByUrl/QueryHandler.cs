using AutoMapper;
using MShare.Framework.Api;
using MShare.Framework.Api.Exceptions;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.Application.SqlClient;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Queries.V1.GetAlbumByUrl
{
	public class QueryHandler : IQueryHandler<GetAlbumByUrlQuery, AlbumResponseDto>
	{
        private readonly IStreamingServiceTypeRecognizer _recognizer;
        private readonly IProxyServiceClientFactory _clientFactory;
        private readonly IMapper _mapper;
        private readonly QueryContext _context;
        private readonly ISqlQueryExecutor _sqlQueryExecutor;
        private readonly IMetadataExtractor _idExtractor;
        private readonly IExecutionContext _executionContext;

        public QueryHandler(
            IStreamingServiceTypeRecognizer recognizer,
            IProxyServiceClientFactory clientFactory,
            IMapper mapper,
            IQueryContext<GetAlbumByUrlQuery, AlbumResponseDto> context,
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

        public async Task<AlbumResponseDto> Handle(GetAlbumByUrlQuery request, CancellationToken cancellationToken)
        {
            var recognizerServiceResult = _recognizer.From(new Uri(request.AlbumUrl));
            BadRequestException.ThrowIf(recognizerServiceResult.IsFail, recognizerServiceResult.FailMessage);

            var response = await GetAlbumFromDatabase(recognizerServiceResult.Data, request.AlbumUrl);

            if (response == null)
            {
                var serviceClient = _clientFactory.Create(recognizerServiceResult.Data);

                var albumResult = await serviceClient.GetAlbumByUrlAsync(request.AlbumUrl, _executionContext.StoreRegion);
                NotFoundException.ThrowIf(albumResult is null, "Album not found");

                _context.ServiceProxyResponse = albumResult;
                _context.ServiceType = recognizerServiceResult.Data;

                response = _mapper.Map<AlbumResponseDto>((albumResult, recognizerServiceResult.Data));
            }

            return response;
        }

        public async Task<AlbumResponseDto?> GetAlbumFromDatabase(StreamingServiceType streamingService, string url)
        {
            var idResult = _idExtractor.ExtractId(url, streamingService, MediaType.Album);

            if (idResult.IsSuccess)
            {
                var sql =
                    $"SELECT " +
                        $"service_type as  ServiceType, source_id AlbumSourceId, " +
                        $"image_url CoverImageUrl, source_url AlbumUrl, " +
                        $"artist_name ArtistName, name AlbumName " +
                    $"FROM album " +
                    $"WHERE service_type = '{streamingService}' " +
                        $"AND source_id='{idResult.Data}' " +
                        $"AND region='{_executionContext.StoreRegion}'";

                return await _sqlQueryExecutor.QueryFirstOrDefaultAsync<AlbumResponseDto>(sql);
            }

            return null;
        }
    }
}

