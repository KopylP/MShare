using System;
using System.IO;
using MShare.Framework.Exceptions;
using MShare.Framework.Types;
using MShare.Framework.WebApi.Exceptions;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Songs
{
    public class MediaTypeRecognizer : IMediaTypeRecognizer
    {
        private readonly IStreamingServiceTypeRecognizer _streamingServiceTypeRecognizer;

        public MediaTypeRecognizer(IStreamingServiceTypeRecognizer streamingServiceTypeRecognizer)
            => _streamingServiceTypeRecognizer = streamingServiceTypeRecognizer;

        public Result<MediaType> From(Uri uri)
        {
            var serviceTypeResult = _streamingServiceTypeRecognizer.From(uri);

            if (serviceTypeResult.IsFail)
                return Result<MediaType>.Fail(serviceTypeResult.FailMessage);

            var mediaTypeResult = serviceTypeResult.Data switch
            {
                StreamingServiceType.Spotify => GetMediaTypeFromSpotify(uri),
                StreamingServiceType.AppleMusic => GetMediaTypeFromApplyMusic(uri),
                StreamingServiceType.YoutubeMusic => Result<MediaType>.Fail("YouTube Music not supported yet."),
                _ => throw new NotSupportedException()
            };

            if (mediaTypeResult.IsFail)
                return Result<MediaType>.Fail(mediaTypeResult.FailMessage);

            return Result<MediaType>
                .Success(mediaTypeResult.Data);
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

            return Result<MediaType>.Fail("Media type is not recognized");
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

            if(pathAndQuery.RemoveFrom('?').Contains("album")
                && (pathAndQuery.Contains("&i=")
                || pathAndQuery.Contains("?i=")))
            {
                return Result<MediaType>.Success(MediaType.Song);
            }

            if (pathAndQuery.RemoveFrom('?').Contains("artist"))
            {
                return Result<MediaType>.Success(MediaType.Artist);
            }

            return Result<MediaType>.Fail("Media type is not recognized");
        }
    }
}

