using System;
using MShare.Songs.Abstractions;
using MShare.Songs.Domain;
using ProxyService.Client;

namespace MShare.Songs.Application.Factories
{
	public interface IProxyServiceClientFactory
	{
		IProxyServiceClient Create(StreamingServiceType type);
	}
}

