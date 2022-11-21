using Flurl;
using Proxy.Api;
using SpotifyProxy.WebService.Infrastructure.Client.Models;
using AutoMapper;
using System.Text;
using MShare.Framework.Extentions;
using Flurl.Http;
using MShare.Framework.WebApi.Exceptions;
using MShare.Framework.Infrastructure.AccessToken;
using MassTransit;
using System.Collections.Generic;

namespace SpotifyProxy.WebService.Infrastructure.Client
{
    internal class SpotifyClient : IStreamingServiceClient
    {
        private readonly string _publicApiUrl;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IMapper _mapper;

        public SpotifyClient(IConfiguration configuration, IAccessTokenProvider provider, IMapper mapper)
        {
            _publicApiUrl = configuration.GetValue<string>("SpotifyPublicApiUrl");
            _accessTokenProvider = provider;
            _mapper = mapper;
        }

        public async Task<SongsResponseDto> FindSongAsync(FindSongsRequestDto request, int limit = 5)
        {
            var searchParam = new StringBuilder();
            searchParam.Append($"track: {request.SongName?.PrepareSongName()} ")
                .AppendIf(!string.IsNullOrWhiteSpace(request.AlbumName), $"+album: {request.AlbumName?.Trim()} ")
                .AppendIf(!string.IsNullOrWhiteSpace(request.ArtistName), $"+artist: {request.ArtistName?.Trim()} ");

            var response = await _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "track")
                .SetQueryParam("q", searchParam)
                .SetQueryParam("limit", GetLimit(request, limit))
                .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);

            return _mapper.Map<SongsResponseDto>(response);
        }

        public async Task<SongResponseDto> GetSongByUrlAsync(GetByUrlRequestDto request)
        {
            var song = await GetTrackAsync(request.Url?.GetSpotifyId());
            var album = await GetAlbumAsync(song.Album.Id);

            return _mapper.Map<SongResponseDto>((song, album));
        }

        public async Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request)
        {
            var response = await _publicApiUrl
                .AppendPathSegment("albums")
                .AppendPathSegment(request.Url?.GetSpotifyId())
                .GetAuthorizedAsync<SpotifyAlbumResponseModel>(_accessTokenProvider);

            return _mapper.Map<AlbumResponseDto>(response);
        }

        private async Task<SpotifyTrackResponseModel> GetTrackAsync(string? id)
        {
            return await _publicApiUrl
                .AppendPathSegment("tracks")
                .AppendPathSegment(id)
                .GetAuthorizedAsync<SpotifyTrackResponseModel>(_accessTokenProvider);
        }

        private async Task<SpotifyAlbumResponseModel> GetAlbumAsync(string? id)
        {
            return await _publicApiUrl
                .AppendPathSegment("albums")
                .AppendPathSegment(id)
                .GetAuthorizedAsync<SpotifyAlbumResponseModel>(_accessTokenProvider);
        }

        private static int GetLimit(FindSongsRequestDto model, int limit)
        {
            if (!string.IsNullOrWhiteSpace(model.ArtistName))
                return 1;

            return limit;
        }

        public async Task<SongResponseDto> GetSongByIsrcAsync(GetByIsrcRequestDto request)
        {
            var response = await _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "track")
                .SetQueryParam("q", $"isrc:{request.Isrc}")
                .SetQueryParam("limit", 1)
                .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);

            if (!response?.Tracks?.Items?.Any() ?? true)
                throw new NotFoundException();

            var song = response.Tracks.Items.First();
            var album = await GetAlbumAsync(song.Album.Id);

            return _mapper.Map<SongResponseDto>((song, album));
        }
    }
}

