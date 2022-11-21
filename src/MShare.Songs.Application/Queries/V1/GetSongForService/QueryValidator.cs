using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MShare.Framework.Api.Shared;
using MShare.Framework.Application.Actions;
using MShare.Framework.Exceptions;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Queries.V1.GetSongForService
{
	public class QueryValidator : IRequestValidator<GetSongForServiceQuery, ListResponseDto<SongResponseDto>>
    {
        private readonly StreamingServiceType[] _availableServices;

        public QueryValidator(IConfiguration configuration)
        {
            _availableServices = configuration
                .GetSection("Services")
                .Get<Service[]>()
                .Where(p => p.IsAvailable)
                .Select(p => p.Type)
                .ToArray();
        }

        public async Task Validate(GetSongForServiceQuery request)
        {
            BadRequestException.ThrowIf(!_availableServices.Contains(request.OriginService), "Origin service is not supported.");
            BadRequestException.ThrowIf(!_availableServices.Contains(request.DestinationService), "Destination service is not supported.");

            await Task.CompletedTask;
        }
    }
}

