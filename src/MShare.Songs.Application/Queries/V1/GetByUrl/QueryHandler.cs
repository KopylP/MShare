using System;
using AutoMapper;
using MediatR;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;
using MShare.Songs.Api.V1.Queries;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;

namespace MShare.Songs.Application.Queries.V1.GetByUrl
{
	public class QueryHandler : IQueryHandler<GetByUrlQuery, GetByUrlResponseDto>
	{
        private readonly IMediator _mediator;
        private readonly QueryContext _context;

        public QueryHandler(IQueryContext<GetByUrlQuery, GetByUrlResponseDto> context, IMediator mediator)
        {
            _mediator = mediator;
            _context = (QueryContext)context;
        }

        public async Task<GetByUrlResponseDto> Handle(GetByUrlQuery request, CancellationToken cancellationToken)
        {
            var response = new GetByUrlResponseDto()
            {
                MediaType = _context.MediaType.ToString()
            };

            if (_context.MediaType == MediaType.Album)
                response.Album = await _mediator.Send(GetAlbumByUrlQuery.Of(request.Url));

            if (_context.MediaType == MediaType.Song)
                response.Song = await _mediator.Send(GetSongByUrlQuery.Of(request.Url));

            response.Services = await GetServices(_context.ServiceType.ToString());

            return response;
        }

        private async Task<ServicesResponseDto.ItemDto[]> GetServices(string mainServiceType)
        {
            var servicesResponse = await _mediator.Send(GetServicesQuery.Instance);

            return servicesResponse.Items
                .OrderBy(p => p.Name)
                .OrderByDescending(p => p.IsAvailable)
                .ThenBy(p => p.Type.ToLower() == mainServiceType.ToLower())
                .ToArray();
        }
    }
}