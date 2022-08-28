using System;
using MShare.Songs.Abstractions;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;
using ProxyService.Client;

namespace MShare.Songs.Infrastructure.ProxyService
{
    internal class ProxyServiceClientFactory : IProxyServiceClientFactory
    {
        public IProxyServiceClient Create(StreamingServiceType type)
        {
            var configuration = GetConfiguration(type);
            return new ProxyServiceClient(configuration);
        }

        private IProxyServiceClientConfiguration GetConfiguration(StreamingServiceType type)
            => type switch
            {
                StreamingServiceType.AppleMusic => new AppleConfiguration(),
                StreamingServiceType.Spotify => new SpotifyConfiguration(),
                _ => throw new NotImplementedException()
            };
    }
}

