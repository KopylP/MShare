using System;
using System.IO;
using Microsoft.Extensions.Localization;
using MShare.Framework.Exceptions;
using MShare.Framework.Types;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;
using MShare.Songs.Resources;
using MShare.Songs.Resources.SharedResource;

namespace MShare.Songs.Infrastructure.Songs
{
    public class MediaTypeRecognizer : IMediaTypeRecognizer
    {
        private readonly IStreamingServiceTypeRecognizer _streamingServiceTypeRecognizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public MediaTypeRecognizer(IStreamingServiceTypeRecognizer streamingServiceTypeRecognizer,
            IStringLocalizer<SharedResource> sharedLocalizer)
            => (_streamingServiceTypeRecognizer, _sharedLocalizer) = (streamingServiceTypeRecognizer, sharedLocalizer);

        public Result<MediaType> From(Uri uri)
        {
            var serviceTypeResult = _streamingServiceTypeRecognizer.From(uri);

            if (serviceTypeResult.IsFail)
                return Result<MediaType>.Fail(serviceTypeResult.FailMessage);

            return serviceTypeResult.Data switch
            {
                StreamingServiceType.Spotify => GetMediaTypeFromSpotify(uri),
                StreamingServiceType.AppleMusic => GetMediaTypeFromApplyMusic(uri),
                _ => throw new NotSupportedException()
            };
        }

        private Result<MediaType> GetMediaTypeFromSpotify(Uri uri)
        {
            var path = uri.PathAndQuery.RemoveFrom('?').ToLower();

            if (path.Contains("album"))
                return Result<MediaType>.Success(MediaType.Album);

            if (path.Contains("track"))
                return Result<MediaType>.Success(MediaType.Song);

            if (path.Contains("artist"))
                return Result<MediaType>.Success(MediaType.Artist);

            return Result<MediaType>.Fail();
        }

        private Result<MediaType> GetMediaTypeFromApplyMusic(Uri uri)
        {
            var pathAndQuery = uri.PathAndQuery;

            if (pathAndQuery.RemoveFrom('?').Contains("album")
                && !pathAndQuery.Contains("&i=")
                && !pathAndQuery.Contains("?i="))
            {
                return Result<MediaType>.Success(MediaType.Album);
            }

            if (pathAndQuery.RemoveFrom('?').Contains("album")
                && (pathAndQuery.Contains("&i=")
                || pathAndQuery.Contains("?i=")))
            {
                return Result<MediaType>.Success(MediaType.Song);
            }

            if (pathAndQuery.RemoveFrom('?').Contains("artist"))
            {
                return Result<MediaType>.Success(MediaType.Artist);
            }

            return Result<MediaType>.Fail();
        }
    }
}

