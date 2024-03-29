﻿using System;
using MShare.Framework.Types;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;

namespace MShare.Songs.Infrastructure.Songs
{
    public class StreamingServiceTypeRecognizer : IStreamingServiceTypeRecognizer
    {
        public Result<StreamingServiceType> From(Uri uri) => uri.Host.ToLower() switch
        {
            "music.apple.com" => Result<StreamingServiceType>.Success(StreamingServiceType.AppleMusic),
            "open.spotify.com" => Result<StreamingServiceType>.Success(StreamingServiceType.Spotify),
            "music.youtube.com" => Result<StreamingServiceType>.Success(StreamingServiceType.YoutubeMusic),
            _ => Result<StreamingServiceType>.Fail("Streaming service is not recognized or not supported yet.")
        };
    }
}

