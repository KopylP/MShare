﻿using System;
using Microsoft.Extensions.Configuration;
using MShare.Framework.Application;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Api.V1.Queries.Dtos;

namespace MShare.Songs.Application.Queries.V1.GetServices
{
    using static ServicesResponseDto;

    public class QueryHandler : IQueryHandler<GetServicesQuery, ServicesResponseDto>
    {
        private readonly IConfiguration _configuration;

        public QueryHandler(IConfiguration configuration) => _configuration = configuration;

        public async Task<ServicesResponseDto> Handle(GetServicesQuery request, CancellationToken cancellationToken)
            => await Task.FromResult(Of(_configuration.GetSection("Services").Get<ItemDto[]>()));
    }
}
