using System;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MShare.Framework.Types;
using MShare.Framework.Types.Addresses;

namespace ProxyService.Client
{
    public class ProxyServiceClient : IProxyServiceClient
    {
        private readonly string _baseUrl;
        public ProxyServiceClient(IProxyServiceClientConfiguration configuration)
        {
            _baseUrl = configuration.BaseUrl;
        }

        public async Task<SongsResponseDto> FindSongAsync(string songName, string artistName, string? albumName)
        {
            var url = _baseUrl
                .AppendPathSegments("api", "Song", "Find")
                .SetQueryParam("songName", songName)
                .SetQueryParam("artistName", artistName)
                .SetQueryParamIf(albumName is not null, "albumName", albumName);


            return await Get<SongsResponseDto>(url);
        }

        public async Task<AlbumResponseDto> GetAlbumByUrlAsync(string url)
        {
            var uri = _baseUrl
               .AppendPathSegments("api", "Album", "Url")
               .SetQueryParam("url", url);

            return await Get<AlbumResponseDto>(uri);
        }

        public async Task<SongResponseDto> GetSongByIdAsync(string id, CountryCode2? region = null)
        {
            var uri = _baseUrl
               .AppendPathSegments("api", "Song", "Id")
               .SetQueryParam("Id", id)
               .SetQueryParam("Region", region?.Code ?? CountryCode2.Invariant);

            return await Get<SongResponseDto>(uri);
        }

        public async Task<SongResponseDto> GetSongByIsrcAsync(string isrc, CountryCode2? region = default)
        {
            var uri = _baseUrl
               .AppendPathSegments("api", "Song", "Isrc")
               .SetQueryParam("Isrc", isrc)
               .SetQueryParam("Region", region?.Code ?? CountryCode2.Invariant);

            return await Get<SongResponseDto>(uri);
        }

        public async Task<SongResponseDto> GetSongByUrlAsync(string url)
        {
            var uri = _baseUrl
                .AppendPathSegments("api", "Song", "Url")
                .SetQueryParam("url", url);

            return await Get<SongResponseDto>(uri);
        }

        private async Task<TResponse> Get<TResponse>(Url url)
        {
            try
            {
                return await url.GetJsonAsync<TResponse>();
            }
            catch (FlurlHttpException ex)
            {
                throw new MShare.Framework.WebApi.Exceptions.ApiException(ex.StatusCode ?? 500, await ex.GetResponseStringAsync());
            }
        }
    }
}

