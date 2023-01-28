using System;
using AutoMapper;
using MShare.Framework.Api;
using MShare.Framework.Api.Exceptions;
using MShare.Framework.Api.Shared;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.Application.SqlClient;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Application.Factories;

namespace MShare.Songs.Application.Queries.V1.GetSongForService
{
	internal partial class QueryHandler : IQueryHandler<GetSongForServiceQuery, ListResponseDto<SongResponseDto>>
	{
        private readonly ISqlQueryExecutor _sqlQueryExecutor;
        private readonly IMapper _mapper;
        private readonly IProxyServiceClientFactory _clientFactory;
        private readonly IExecutionContext _executionContext;
        private readonly QueryContext _context;

        public QueryHandler(
            ISqlQueryExecutor sqlQueryExecutor,
            IProxyServiceClientFactory clientFactory,
            IMapper mapper,
            IExecutionContext executionContext,
            IQueryContext<GetSongForServiceQuery, ListResponseDto<SongResponseDto>> context)
		{
            _executionContext = executionContext;
            _sqlQueryExecutor = sqlQueryExecutor;
            _clientFactory = clientFactory;
            _mapper = mapper;
            _context = (QueryContext)context;
		}

        public async Task<ListResponseDto<SongResponseDto>> Handle(GetSongForServiceQuery request, CancellationToken cancellationToken)
        {
            var isrc = await GetIsrcAsync(request);
            NotFoundException.ThrowIf(string.IsNullOrWhiteSpace(isrc));

            return ListResponseDto<SongResponseDto>.Of(await GetSongAsync(isrc, request));
        }

        private async Task<string> GetIsrcAsync(GetSongForServiceQuery request)
        {
            var isrc = await GetIsrcFromDatabase(request);

            if (isrc == null)
                isrc = await GetIsrcFromClient(request);

            return isrc;
        }

        private async Task<string> GetIsrcFromDatabase(GetSongForServiceQuery request)
        {
            return await _sqlQueryExecutor.QueryFirstOrDefaultAsync<string>(IsrcSqlBuilder.Of(_executionContext.StoreRegion, request.OriginService, request.SourceId));
        }

        private async Task<string> GetIsrcFromClient(GetSongForServiceQuery request)
        {
            var client = _clientFactory.Create(request.OriginService);
            var response = await client.GetSongByIdAsync(request.SourceId, _executionContext.StoreRegion);

            return response.Song.Isrc;
        }

        private async Task<SongResponseDto> GetSongAsync(string isrc, GetSongForServiceQuery request)
        {
            var response = await GetSongFromDatabaseAsync(isrc, request);

            if (response == null)
                response = await GetSongFromClientAsync(isrc, request);

            return response;
        }

        private async Task<SongResponseDto> GetSongFromDatabaseAsync(string isrc, GetSongForServiceQuery request)
        {
            return await _sqlQueryExecutor.QueryFirstOrDefaultAsync<SongResponseDto>(SongByIsrcSqlBuilder.Of(_executionContext.StoreRegion, request.DestinationService, isrc));
        }

        private async Task<SongResponseDto> GetSongFromClientAsync(string isrc, GetSongForServiceQuery request)
        {
            var client = _clientFactory.Create(request.DestinationService);
            var response = await client.GetSongByIsrcAsync(isrc, _executionContext.StoreRegion);

            _context.ServiceProxyResponse = response;
            _context.ServiceType = request.DestinationService;

            return _mapper.Map<SongResponseDto>((response, request.DestinationService));
        }
    }
}