using AppleProxy.WebService.Helpers;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl.Http;
using MShare.Framework.WebApi.Exceptions;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request)
        {
            var region = request.Url?.GetAppleRegion() ?? Region.Invariant;

            var response = await _publicApiUrl
                .AppendPathSegmentIf(region != Region.Invariant, region)
                .AppendPathSegment("lookup")
                .SetQueryParam("id", request.Url?.GetAppleCollectionId())
                .SetQueryParam("limit", 1)
                .GetJsonWithRetryAsync<AppleAlbumListResponseModel>(_retryPolicy);

            if (!response.Results.Any())
                throw new NotFoundException();

            return _mapper.Map<AlbumResponseDto>((response.Results.First(), region));
        }
    }
}

