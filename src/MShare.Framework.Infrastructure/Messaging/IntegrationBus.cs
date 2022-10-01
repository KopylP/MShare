using System;
using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Framework.Application.Context;

namespace MShare.Framework.Infrastructure.Messaging
{
	public class IntegrationBus : IIntegrationBus
	{
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public IntegrationBus(IPublishEndpoint publishEndpoint, IServiceProvider serviceProvider)
		{
            _publishEndpoint = publishEndpoint;
            _serviceProvider = serviceProvider;
		}

        public async Task Publish<T>(T message, CancellationToken cancellationToken = default) where T: class, IIntegrationEvent
        {
            var executionContext = _serviceProvider.GetService<IExecutionContext>();
            await _publishEndpoint.Publish<T>(message, context => {

                if (executionContext is not null)
                {
                    context.Headers.Set(nameof(IExecutionContext), executionContext);
                }
            });
        }
    }
}

