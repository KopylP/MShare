using System;
using AppleProxy.WebService.Infrastructure.Client.Models;
using Flurl;
using Flurl.Http;
using Proxy.Api;
using Unidecode.NET;

namespace AppleProxy.WebService.Infrastructure.Client
{
	public partial class AppleClient
	{
        public async Task<SongsResponseDto> FindAsync(FindSongsRequestDto request, int limit)
        {
            AppleTrackListResponseModel response = AppleTrackListResponseModel.Empty;

            if (!string.IsNullOrWhiteSpace(request.ArtistName))
                response = await FindByArtistAsync(request, limit);

            else if (!string.IsNullOrWhiteSpace(request.AlbumName))
                response = await FindByAlbumAsync(request, limit);

            else
                response = await FindBySongAsync(request, limit);

            return _mapper.Map<SongsResponseDto>(response);
        }

        public async Task<AppleTrackListResponseModel> FindByArtistAsync(FindSongsRequestDto request, int limit)
        {
            var words = new string[] { request.ArtistName.Trim(), request.SongName.Trim() };

            var term = string.Join(" + ", words);

            var url = GetBaseUrl(term, request, limit)
                .SetQueryParam("attribute", "artistTerm");

            var response = await url.GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);

            if (!response.Results?.Any() ?? true)
            {
                url.SetQueryParam("attribute", "mixTerm");
                response = await url.GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);
            }

            return response ?? AppleTrackListResponseModel.Empty;
        }

        public async Task<AppleTrackListResponseModel> FindByAlbumAsync(FindSongsRequestDto request, int limit)
        {
            var words = new string[] { request.AlbumName?.Trim() ?? "", request.SongName.Trim() };

            var term = string.Join(" + ", words);

            var url = GetBaseUrl(term, request, limit)
                .SetQueryParam("attribute", "albumTerm");

            var response = await url.GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);

            if (!response.Results?.Any() ?? true)
            {
                url.SetQueryParam("attribute", "mixTerm");
                response = await url.GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);
            }

            response.Results = response?.Results?.Take(limit)?.ToArray()
                ?? Array.Empty<AppleTrackResponseModel>();

            return response ?? AppleTrackListResponseModel.Empty;
        }

        public async Task<AppleTrackListResponseModel> FindBySongAsync(FindSongsRequestDto request, int limit)
        {
            var url = GetBaseUrl(request.SongName.Trim(), request, limit)
                .SetQueryParam("attribute", "songTerm");

            var response = await url.GetJsonWithRetryAsync<AppleTrackListResponseModel>(_retryPolicy);

            response.Results = response?.Results?.Take(limit)?.ToArray()
                ?? Array.Empty<AppleTrackResponseModel>();

            return response ?? AppleTrackListResponseModel.Empty;
        }

        private Url GetBaseUrl(string term, FindSongsRequestDto request, int limit)
        {
            return _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("term", term)
                .SetQueryParam("limit", limit)
                .SetQueryParam("media", "music")
                .SetQueryParam("entity", "musicTrack");
        }
    }
}

