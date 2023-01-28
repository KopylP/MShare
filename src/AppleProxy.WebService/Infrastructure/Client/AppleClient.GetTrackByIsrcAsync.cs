using System;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl;
using Flurl.Http;
using MShare.Framework.Api.Exceptions;
using MShare.Framework.Types.Addresses;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<SongResponseDto> GetTrackByIsrcAsync(GetByIsrcRequestDto request)
        {
            var region = GetRegion(request.Region);

            var response = await _publicApiUrl
                .AppendPathSegment("catalog")
                .AppendPathSegment(region)
                .AppendPathSegment("songs")
                .SetQueryParam("filter[isrc]", request.Isrc)
                .GetAuthorizedAsync<AppleTrackListResponseModel>(_accessTokenProvider);

            if (!response.Data.Any())
                throw new NotFoundException();

            var result = _mapper.Map<SongResponseDto>((response.Data.First(), region));
            var album = await GetAlbumByUrl(GetByUrlRequestDto.Of(result.Song.SourceUrl.RemoveFrom('?'), region));
            result.Album = album.Album;

            return result;
        }
    }
}

