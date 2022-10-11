using System;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Songs
{
    public class IdExtractor : IIdExtractor
    {
        public Result<string> Extract(string url, StreamingServiceType streamingServiceType, MediaType mediaType) => (streamingServiceType, mediaType) switch
        {
            (StreamingServiceType.AppleMusic, MediaType.Song) => Result<string>.Success(url.GetAppleSongId()),
            (StreamingServiceType.AppleMusic, MediaType.Album) => Result<string>.Success(url.GetAppleCollectionId()),

            (StreamingServiceType.Spotify, MediaType.Song) or (StreamingServiceType.Spotify, MediaType.Album)
                => Result<string>.Success(url.GetSpotifyId()),

            _ => throw new NotSupportedException()
        };
     }
}

