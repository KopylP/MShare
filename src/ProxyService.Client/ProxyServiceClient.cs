using System;
using ProjectService.Client.Api;

namespace ProxyService.Client
{
    public class ProxyServiceClient : IProxyServiceClient
    {
        private readonly swaggerClient _client;

        public ProxyServiceClient(IProxyServiceClientConfiguration configuration, HttpClient client)
            => _client = new swaggerClient(configuration.BaseUrl, client);

        public async Task<SongsResponseDto> FindAsync(string songName, string artistName, string? albumName)
            => await _client.FindAsync(songName, artistName, albumName);

        public async Task<SongResponseDto> GetSongByUrlAsync(string url)
            => await _client.UrlAsync(new Uri(url));
    }
}

