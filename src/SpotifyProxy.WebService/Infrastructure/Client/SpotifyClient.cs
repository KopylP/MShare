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
using MShare.Framework.Types.Addresses;

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

        public async Task<SongsResponseDto> FindTrackAsync(FindSongsRequestDto request)
        {
            throw new NotImplementedException();
            //var searchParam = new StringBuilder();
            //searchParam.Append($"track: {request.SongName?.PrepareSongName()} ")
            //    .AppendIf(!string.IsNullOrWhiteSpace(request.AlbumName), $"+album: {request.AlbumName?.Trim()} ")
            //    .AppendIf(!string.IsNullOrWhiteSpace(request.ArtistName), $"+artist: {request.ArtistName?.Trim()} ");

            //var response = await _publicApiUrl
            //    .AppendPathSegment("search")
            //    .SetQueryParam("type", "track")
            //    .SetQueryParam("q", searchParam)
            //    .SetQueryParam("limit", GetLimit(request, 5))
            //    .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);

            //return _mapper.Map<SongsResponseDto>(response);
        }

        public async Task<SongResponseDto> GetTrackByUrlAsync(GetByUrlRequestDto request)
        {
            return await GetTrackByIdAsync(GetByIdRequestDto.Of(request.Url?.GetSpotifyId() ?? "", request.Region));
        }

        public async Task<AlbumResponseDto> GetAlbumByUrl(GetByUrlRequestDto request)
        {
            var region = GetRegion(request.Region);
            var response = await GetAlbumAsync(request.Url.GetSpotifyId(), region);

            return _mapper.Map<AlbumResponseDto>((response, region));
        }

        public async Task<SongResponseDto> GetTrackByIsrcAsync(GetByIsrcRequestDto requestDto)
        {
            var region = GetRegion(requestDto.Region);

            var request = _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "track")
                .SetQueryParam("q", $"isrc:{requestDto.Isrc}")
                .SetQueryParam("market", region)
                .SetQueryParam("limit", 1);

            var response = await request
                .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);

            if (!response?.Tracks?.Items?.Any() ?? true)
            {
                request.SetQueryParam("q", $"isrc:{requestDto.Isrc?.ToLowerInvariant()}");
                response = await request
                    .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);
            }

            if (!response?.Tracks?.Items?.Any() ?? true)
            {
                request.SetQueryParam("q", $"isrc:{requestDto.Isrc?.ToUpperInvariant()}");
                response = await request
                    .GetAuthorizedAsync<SpotifySearchTrackResponseModel>(_accessTokenProvider);
            }

            if (!response?.Tracks?.Items?.Any() ?? true)
                throw new NotFoundException();

            var song = response.Tracks.Items.First();
            var album = await GetAlbumAsync(song.Album.Id, region);

            return _mapper.Map<SongResponseDto>((song, album, region));
        }

        public async Task<SongResponseDto> GetTrackByIdAsync(GetByIdRequestDto request)
        {
            var region = GetRegion(request.Region);
            var song = await GetTrackAsync(request.Id, region);
            var album = await GetAlbumAsync(song.Album.Id, region);

            return _mapper.Map<SongResponseDto>((song, album, region));
        }

        public async Task<AlbumResponseDto> GetAlbumByUpc(GetByUpcRequestDto requestDto)
        {
            var region = GetRegion(requestDto.Region);

            var request = _publicApiUrl
                .AppendPathSegment("search")
                .SetQueryParam("type", "album")
                .SetQueryParam("q", $"upc:{requestDto.Upc}")
                .SetQueryParam("market", region)
                .SetQueryParam("limit", 1);

            var response = await request
                .GetAuthorizedAsync<SpotifySearchAlbumsResponseModel>(_accessTokenProvider);

            if (!response?.Albums?.Items?.Any() ?? true)
                throw new NotFoundException();

            var albumId = response.Albums.Items.Select(p => p.Id).First();

            return _mapper.Map<AlbumResponseDto>((await GetAlbumAsync(albumId, region), region));
        }

        public async Task<AlbumResponseDto> GetAlbumById(GetByIdRequestDto request)

        {
            var region = GetRegion(request.Region);
            var album = await GetAlbumAsync(request.Id, region);
            return _mapper.Map<AlbumResponseDto>((album, region));
        }

        private async Task<SpotifyTrackResponseModel> GetTrackAsync(string? id, string region)
        {
            return await _publicApiUrl
                .AppendPathSegment("tracks")
                .AppendPathSegment(id)
                .SetQueryParam("market", region)
                .GetAuthorizedAsync<SpotifyTrackResponseModel>(_accessTokenProvider);
        }

        private async Task<SpotifyAlbumResponseModel> GetAlbumAsync(string? id, string region)
        {
            return await _publicApiUrl
                .AppendPathSegment("albums")
                .AppendPathSegment(id)
                .SetQueryParam("market", region)
                .GetAuthorizedAsync<SpotifyAlbumResponseModel>(_accessTokenProvider);
        }

        private static int GetLimit(FindSongsRequestDto model, int limit)
        {
            if (!string.IsNullOrWhiteSpace(model.ArtistName))
                return 1;

            return limit;
        }

        private string GetRegion(string region) => region != CountryCode2.Invariant.ToString() ? region : ApiConstants.DefaultRegion;
    }
}

