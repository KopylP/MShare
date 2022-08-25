using System;
using ProxyService.Client;

namespace MShare.Songs.Infrastructure.ProxyService
{
    internal class AppleConfiguration : IProxyServiceClientConfiguration
    {
        public string BaseUrl => "http://appleproxy";
    }
}

