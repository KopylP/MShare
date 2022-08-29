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
        public async Task<SongResponseDto> GetByUrlAsync(GetSongByUrlRequestDto request)
        {
            var response = await _publicApiUrl
                .AppendPathSegment("lookup")
                .SetQueryParam("id", request.Url?.GetAppleSongId())
                .SetQueryParam("limit", 1)
                .GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);

            if (!response.Results.Any())
                throw new NotFoundException();

            return _mapper.Map<SongResponseDto>(response.Results.First());
        }
    }
}

