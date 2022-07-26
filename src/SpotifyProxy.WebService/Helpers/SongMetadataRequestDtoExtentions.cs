using System;
using Proxy.Api;

namespace SpotifyProxy.WebService.Helpers
{
    public static class SongMetadataRequestDtoExtentions
    {
        public static bool IsSearchByName(this SongMetadataRequestDto dto)
            => string.IsNullOrWhiteSpace(dto.Url);
    }
}

