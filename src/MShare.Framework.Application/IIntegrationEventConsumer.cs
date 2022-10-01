using System;
using MassTransit;
using MShare.Framework.Api;

namespace MShare.Framework.Application
{
	public interface IIntegrationEventConsumer<T> : IConsumer<T> where T: class, IIntegrationEvent
    {
	}
}