using System;
using Microsoft.Extensions.Localization;
using MShare.Framework.Application.Actions;
using MShare.Framework.Application.Context;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Api.Queries.Dtos.V1;
using MShare.Songs.Api.Queries.V1;
using MShare.Songs.Domain;
using MShare.Songs.Resources.SharedResource;

namespace MShare.Songs.Application.Queries.V1.GetByUrl
{
    public class BeforeActionHandler : IBeforeActionHandler<GetByUrlQuery, GetByUrlResponseDto>
    {
        private readonly IMediaTypeRecognizer _mediaTypeRecognizer;
        private readonly IStreamingServiceTypeRecognizer _streamingServiceTypeRecognizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly QueryContext _queryContext;

        public BeforeActionHandler(IMediaTypeRecognizer mediaTypeRecognizer,
            IStreamingServiceTypeRecognizer streamingServiceTypeRecognizer,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IRequestContext<GetByUrlQuery, GetByUrlResponseDto> context)
        {
            _mediaTypeRecognizer = mediaTypeRecognizer;
            _streamingServiceTypeRecognizer = streamingServiceTypeRecognizer;
            _queryContext = context as QueryContext;
            _sharedLocalizer = sharedLocalizer;
        }

        public void Handle(GetByUrlQuery request)
        {
            var streamingServiceResult = _streamingServiceTypeRecognizer.From(request.Url);
            BadRequestException.ThrowIf(streamingServiceResult.IsFail, "Streaming service is not recognized or not supported yet.");
            BadRequestException.ThrowIf(streamingServiceResult.Data == StreamingServiceType.YoutubeMusic, _sharedLocalizer[SharedResource.YouTubeMusicNotSupported]);

            var mediaTypeResult = _mediaTypeRecognizer.From(new Uri(request.Url));
            BadRequestException.ThrowIf(mediaTypeResult.IsFail, mediaTypeResult.FailMessage);
            BadRequestException.ThrowIf(mediaTypeResult.Data == MediaType.Artist, "Artist profile sharing does not supported yet.");

            _queryContext.MediaType = mediaTypeResult.Data;
            _queryContext.ServiceType = streamingServiceResult.Data;
        }
    }
}