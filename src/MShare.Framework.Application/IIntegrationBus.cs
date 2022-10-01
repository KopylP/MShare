using System;
using MShare.Framework.Api;

namespace MShare.Framework.Application
{
	public interface IIntegrationBus
	{
		public Task Publish<T>(T message, CancellationToken cancellationToken = default) where T: class, IIntegrationEvent;
	}
}