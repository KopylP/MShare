using System;
using ProxyService.Client;

namespace MShare.Songs.Infrastructure.ProxyService
{
    internal class SpotifyConfiguration : IProxyServiceClientConfiguration
    {
        public string BaseUrl => "http://spotifyproxy";
    }
}

