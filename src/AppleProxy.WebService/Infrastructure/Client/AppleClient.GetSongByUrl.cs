using System;
using AppleProxy.WebService.Helpers;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl;
using Flurl.Http;
using MShare.Framework.WebApi.Exceptions;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<SongResponseDto> GetSongByUrlAsync(GetByUrlRequestDto request)
        {
            var region = request.Url?.GetAppleRegion() ?? Region.Invariant;

            var response = await _publicApiUrl
                .AppendPathSegmentIf(region != Region.Invariant, region)
                .AppendPathSegment("lookup")
                .SetQueryParam("id", request.Url?.GetAppleCollectionId())
                .SetQueryParam("limit", 1)
                .GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);

            if (!response.Results.Any())
                throw new NotFoundException();

            return _mapper.Map<SongResponseDto>(response.Results.First());
        }
    }
}

