using System;
using AutoMapper;
using MShare.Framework.Api;
using MShare.Framework.Api.Shared;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.Application.SqlClient;
using MShare.Framework.WebApi.Exceptions;
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

        public QueryHandler(
            ISqlQueryExecutor sqlQueryExecutor,
            IProxyServiceClientFactory clientFactory,
            IMapper mapper,
            IExecutionContext executionContext)
		{
            _executionContext = executionContext;
            _sqlQueryExecutor = sqlQueryExecutor;
            _clientFactory = clientFactory;
            _mapper = mapper;
		}

        public async Task<ListResponseDto<SongResponseDto>> Handle(GetSongForServiceQuery request, CancellationToken cancellationToken)
        {
            var isrc = await _sqlQueryExecutor.QueryFirstOrDefaultAsync<string>(SqlBuilder.Of(_executionContext.StoreRegion, request.OriginService, request.SourceId));
            var destinationService = _mapper.Map<StreamingServiceType>(request.DestinationService);
            NotFoundException.ThrowIf(string.IsNullOrWhiteSpace(isrc));

            var client = _clientFactory.Create(destinationService);
            var response = await client.GetSongByIsrcAsync(isrc, _executionContext.StoreRegion);

            return ListResponseDto<SongResponseDto>.Of(_mapper.Map<SongResponseDto>((response, destinationService)));
        }
    }
}