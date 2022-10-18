using System;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Songs
{
    public class IdExtractor : IMetadataExtractor
    {
        public Result<string> ExtractId(string url, StreamingServiceType streamingServiceType, MediaType mediaType) => (streamingServiceType, mediaType) switch
        {
            (StreamingServiceType.AppleMusic, MediaType.Song) => Result<string>.Success(url.GetAppleSongId()),
            (StreamingServiceType.AppleMusic, MediaType.Album) => Result<string>.Success(url.GetAppleCollectionId()),

            (StreamingServiceType.Spotify, MediaType.Song) or (StreamingServiceType.Spotify, MediaType.Album)
                => Result<string>.Success(url.GetSpotifyId()),

            _ => throw new NotSupportedException()
        };

        public Result<CountryCode2> ExtractRegion(string url, StreamingServiceType streamingServiceType, MediaType mediaType)
        {
            try
            {
                if (streamingServiceType == StreamingServiceType.AppleMusic)
                    return Result<CountryCode2>.Success(url.GetAppleRegion());

                return Result<CountryCode2>.Success(CountryCode2.Invariant);
            }
            catch (Exception ex)
            {
                return Result<CountryCode2>.Fail(ex.Message);
            }
        }
    }
}

