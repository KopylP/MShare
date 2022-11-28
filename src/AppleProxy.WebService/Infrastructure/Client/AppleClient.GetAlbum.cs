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
        public async Task<AlbumResponseDto> GetAlbumById(GetByIdRequestDto request)
        {
            var region = GetRegion(request.Region);

            var response = await _publicApiUrl
                .AppendPathSegment("catalog")
                .AppendPathSegment(region)
                .AppendPathSegment("albums")
                .AppendPathSegment(request.Id)
                .GetAuthorizedAsync<AppleAlbumListResponseModel>(_accessTokenProvider);

            if (!response.Data.Any())
                throw new NotFoundException();

            return _mapper.Map<AlbumResponseDto>((response.Data.First(), region));
        }

        public async Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request)
        {
            var id = request.Url?.GetAppleCollectionId();

            return await GetAlbumById(GetByIdRequestDto.Of(id, GetRegion(request.Region)));
        }
    }
}

