using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.Application.SqlClient;
using MShare.Framework.WebApi.Exceptions;
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

        public QueryHandler(
            IStreamingServiceTypeRecognizer recognizer,
            IProxyServiceClientFactory clientFactory,
            IMapper mapper,
            IRequestContext<GetAlbumByUrlQuery, AlbumResponseDto> context,
            ISqlQueryExecutor sqlQueryExecutor,
            IMetadataExtractor idExtractor)
        {
            _recognizer = recognizer;
            _clientFactory = clientFactory;
            _mapper = mapper;
            _context = (QueryContext)context;
            _sqlQueryExecutor = sqlQueryExecutor;
            _idExtractor = idExtractor;
        }

        public async Task<AlbumResponseDto> Handle(GetAlbumByUrlQuery request, CancellationToken cancellationToken)
        {
            var recognizerServiceResult = _recognizer.From(new Uri(request.AlbumUrl));
            BadRequestException.ThrowIf(recognizerServiceResult.IsFail, recognizerServiceResult.FailMessage);

            var response = await GetAlbumFromDatabase(recognizerServiceResult.Data, request.AlbumUrl);

            if (response == null)
            {
                var serviceClient = _clientFactory.Create(recognizerServiceResult.Data);

                var albumResult = await serviceClient.GetAlbumByUrlAsync(request.AlbumUrl);
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
            var regionResult = _idExtractor.ExtractRegion(url, streamingService, MediaType.Album);

            if (idResult.IsSuccess && regionResult.IsSuccess)
            {
                var sql =
                    $"SELECT " +
                        $"service_type as  ServiceType, source_id AlbumSourceId, " +
                        $"image_url CoverImageUrl, source_url AlbumUrl, " +
                        $"artist_name ArtistName, name AlbumName " +
                    $"FROM album " +
                        $"WHERE service_type = '{streamingService}' " +
                        $"AND source_id='{idResult.Data}' " +
                        $"AND region='{regionResult.Data}'";

                return await _sqlQueryExecutor.QueryFirstOrDefaultAsync<AlbumResponseDto>(sql);
            }

            return null;
        }
    }
}

