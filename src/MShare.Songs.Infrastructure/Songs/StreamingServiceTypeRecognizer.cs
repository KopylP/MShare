using System;
using MShare.Framework.Types;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Songs
{
    public class StreamingServiceTypeRecognizer : IStreamingServiceTypeRecognizer
    {
        public Result<StreamingServiceType> From(Uri uri) => uri.Host.ToLower() switch
        {
            "itunes.apple.com" => Result<StreamingServiceType>.Success(StreamingServiceType.Apple),
            "api.spotify.com" => Result<StreamingServiceType>.Success(StreamingServiceType.Spotify),
            _ => Result<StreamingServiceType>.Fail("Streaming service not recognized")
        };
    }
}

