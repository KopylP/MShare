using System;
using Microsoft.AspNetCore.Mvc;

namespace ProxyService.Client
{
    public class ProxyServiceClient : IProxyServiceClient
    {
        private readonly swaggerClient _client;

        public ProxyServiceClient(IProxyServiceClientConfiguration configuration, HttpClient client)
            => _client = new swaggerClient(configuration.BaseUrl, client);

        public async Task<SongsResponseDto> FindAsync(string songName, string artistName, string? albumName)
        {
            try
            {
                return await _client.FindAsync(songName, artistName, albumName);
            }
            catch (ApiException<ProblemDetails> exception)
            {
                throw new MShare.Framework.WebApi.Exceptions.ApiException(exception.StatusCode, exception.Response);
            }
        }

        public async Task<SongResponseDto> GetSongByUrlAsync(string url)
        {
            try
            {
                return await _client.UrlAsync(new Uri(url));
            }
            catch (ApiException exception)
            {
                throw new MShare.Framework.WebApi.Exceptions.ApiException(exception.StatusCode, exception.Response);
            }
        }
    }
}

