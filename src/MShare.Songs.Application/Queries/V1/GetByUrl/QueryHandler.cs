﻿using System;
using AutoMapper;
using MediatR;
using MShare.Framework.Application;
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
        private readonly IMediaTypeRecognizer _recognizer;
        private readonly IMediator _mediator;

        public QueryHandler(IMediaTypeRecognizer recognizer, IMediator mediator)
        {
            _recognizer = recognizer;
            _mediator = mediator;
        }

        public async Task<GetByUrlResponseDto> Handle(GetByUrlQuery request, CancellationToken cancellationToken)
        {
            var mediaTypeResult = _recognizer.From(new Uri(request.Url));
            BadRequestException.ThrowIf(mediaTypeResult.IsFail, mediaTypeResult.FailMessage);
            BadRequestException.ThrowIf(mediaTypeResult.Data == MediaType.Artist, "Share artist profile does not supported yet.");

            var response = new GetByUrlResponseDto()
            {
                MediaType = mediaTypeResult.Data.ToString()
            };

            if (mediaTypeResult.Data == MediaType.Album)
                response.Album = await _mediator.Send(GetAlbumByUrlQuery.Of(request.Url));

            if (mediaTypeResult.Data == MediaType.Song)
                response.Song = await _mediator.Send(GetSongByUrlQuery.Of(request.Url));

            response.Services = (await _mediator.Send(GetServicesQuery.Instance)).Items;

            return response;
        }
    }
}