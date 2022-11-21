﻿using System;
using AppleProxy.WebService.Helpers;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl;
using Flurl.Http;
using MShare.Framework.Types.Addresses;
using MShare.Framework.WebApi.Exceptions;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<SongResponseDto> GetSongByUrlAsync(GetByUrlRequestDto request)
        {
            var region = request.Url?.GetAppleRegion() ?? CountryCode2.Us.ToString();

            var response = await _publicApiUrl
                .AppendPathSegment("catalog")
                .AppendPathSegment(region)
                .AppendPathSegment("songs")
                .AppendPathSegment(request.Url?.GetAppleSongId())
                .GetAuthorizedAsync<AppleTrackListResponseModel>(_accessTokenProvider);

            if (!response.Data.Any())
                throw new NotFoundException();

            var album = await GetAlbumByUrl(request);
            var result = _mapper.Map<SongResponseDto>((response.Data.First(), region));
            result.Album = album.Album;

            return result;
        }
    }
}

