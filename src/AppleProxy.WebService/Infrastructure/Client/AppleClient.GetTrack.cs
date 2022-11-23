using System;
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
        public async Task<SongResponseDto> GetTrackByIdAsync(GetByIdRequestDto request)
        {
            var region = request.Region != CountryCode2.Invariant.ToString() ? request.Region : CountryCode2.Us.ToString();

            var response = await _publicApiUrl
                .AppendPathSegment("catalog")
                .AppendPathSegment(region)
                .AppendPathSegment("songs")
                .AppendPathSegment(request.Id)
                .GetAuthorizedAsync<AppleTrackListResponseModel>(_accessTokenProvider);

            if (!response.Data.Any())
                throw new NotFoundException();

            var url = response.Data.First().Attributes.Url;
            var album = await GetAlbumByUrl(GetByUrlRequestDto.Of(url));
            var result = _mapper.Map<SongResponseDto>((response.Data.First(), request.Region));
            result.Album = album.Album;

            return result;
        }

        public async Task<SongResponseDto> GetTrackByUrlAsync(GetByUrlRequestDto request)
        {
            var id = request.Url?.GetAppleSongId() ?? "";
            var region = request.Url?.GetAppleRegion() ?? CountryCode2.Us.ToString();

            return await GetTrackByIdAsync(GetByIdRequestDto.Of(id, region));
        }
    }
}

