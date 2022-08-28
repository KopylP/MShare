using AppleProxy.WebService.Helpers;
using AppleProxy.WebService.Infrastructure.Client.Models;
using AutoMapper;
using Flurl;
using Flurl.Http;
using MShare.Framework.Infrastructure.Clients;
using MShare.Framework.WebApi.Exceptions;
using Proxy.Api;

namespace AppleProxy.WebService.Infrastructure.Client
{
    public class AppleClient : IStreamingServiceClient
    {
        private readonly string _publicApiUrl;
        private readonly IMapper _mapper;

        public AppleClient(IConfiguration configuration, IMapper mapper)
        {
            _publicApiUrl = configuration.GetValue<string>("ApplePublicApiUrl");
            _mapper = mapper;
        }

        public async Task<SongsResponseDto> FindAsync(FindSongsRequestDto request, int limit)
        {
            var words = Enumerable.Empty<string>();

            if (!string.IsNullOrWhiteSpace(request.AlbumName))
                words = words.Union(request.AlbumName.Replace("-", "").Split(" ", StringSplitOptions.RemoveEmptyEntries));

            if (!string.IsNullOrWhiteSpace(request.ArtistName))
                words = words.Union(new string[] { request.ArtistName });

            words = words.Union(new string[] { request.SongName });

            var term = string.Join(" + ", words);

            var response = await _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("term", term)
                .SetQueryParam("limit", GetLimit(request, limit))
                .SetQueryParam("media", "music")
                .SetQueryParamIf(!string.IsNullOrWhiteSpace(request.ArtistName), "attribute", "artistTerm")
                .SetQueryParamIf(!string.IsNullOrWhiteSpace(request.AlbumName), "attribute", "albumTerm")
                .SetQueryParamIf(
                    string.IsNullOrWhiteSpace(request.AlbumName)
                    && string.IsNullOrWhiteSpace(request.ArtistName),
                    "attribute",
                    "songTerm")
                .SetQueryParam("entity", "musicTrack")
                .GetJsonAsync<AppleTrackListResponseModel>();

            return _mapper.Map<SongsResponseDto>(response);
        }

        public async Task<SongResponseDto> GetByUrlAsync(GetSongByUrlRequestDto request)
        {
            var response = await _publicApiUrl
                .AppendPathSegment("lookup")
                .SetQueryParam("id", request.Url?.GetAppleSongId())
                .SetQueryParam("limit", 1)
                .GetJsonAsync<AppleTrackListResponseModel>();

            if (!response.Results.Any())
                throw new NotFoundException();

            return _mapper.Map<SongResponseDto>(response.Results.First());
        }

        private static int GetLimit(FindSongsRequestDto model, int limit)
        {
            if (!string.IsNullOrWhiteSpace(model.ArtistName))
                return 1;

            return limit;
        }
    }
}

