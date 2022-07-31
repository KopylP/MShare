using Flurl;
using Proxy.Api;
using SpotifyProxy.WebService.Infrastructure.Client.Models;
using SpotifyProxy.WebService.Helpers;
using AutoMapper;
using System.Text;
using MShare.Framework.Extentions;

namespace SpotifyProxy.WebService.Infrastructure.Client
{
    internal class SpotifyClient : IStreamingServiceClient
    {
        private const int LIMIT = 5;
        private readonly string _publicApiUrl;
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;

        public SpotifyClient(IConfiguration configuration, IServiceProvider provider, IMapper mapper)
        {
            _publicApiUrl = configuration.GetValue<string>("SpotifyPublicApiUrl");
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<SongsResponseDto> FindAsync(FindSongsRequestDto request)
        {
            var searchParam = new StringBuilder();
            searchParam.Append($"track: {request.SongName} ")
                .AppendIf(!string.IsNullOrWhiteSpace(request.AlbumName), $"+album: {request.AlbumName} ")
                .AppendIf(!string.IsNullOrWhiteSpace(request.ArtistName), $"+artist: {request.ArtistName} ");

            var response = await _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "track")
                .SetQueryParam("q", searchParam)
                .SetQueryParam("limit", GetLimit(request))
                .GetAuthorizedAsync<SpotifySearchResponseModel>(_provider);           

            return _mapper.Map<SongsResponseDto>(response);
        }

        public async Task<SongResponseDto> GetByUrlAsync(GetSongByUrlRequestDto request)
        {
            var response = await _publicApiUrl
                .AppendPathSegment("tracks")
                .AppendPathSegment(request.Url?.GetSpotifySongId())
                .GetAuthorizedAsync<SpotifyTrackResponseModel>(_provider);

            return _mapper.Map<SongResponseDto>(response);
        }

        private static int GetLimit(FindSongsRequestDto model)
        {
            if (!string.IsNullOrWhiteSpace(model.ArtistName))
                return 1;

            return LIMIT;
        }
    }
}

