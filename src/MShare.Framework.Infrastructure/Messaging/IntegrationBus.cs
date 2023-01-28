using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Api;
using MShare.Framework.Application;
using MShare.Framework.Exceptions;

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

            await _publishEndpoint.Publish(message, context => {

                if (executionContext is not null)
                {
                    context.Headers.Set(nameof(IExecutionContext), executionContext);
                }
            });
        }

        public async Task<TResponse> Request<TResponse, TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest: class, IIntegrationRequest<TResponse>
            where TResponse: class
        {
            var client = _serviceProvider.GetService<IRequestClient<TRequest>>();
            var executionContext = _serviceProvider.GetService<IExecutionContext>();

            Thrower.ThrowIf<InvalidOperationException>(
                client is null,
                $"Service of type {typeof(IRequestClient<TRequest>)} was not found in dependency container.");

            var response = await client.GetResponse<TResponse>(request, x => x.UseExecute(context =>
            {
                if (executionContext is not null)
                {
                    context.Headers.Set(nameof(IExecutionContext), executionContext);
                }
            }), cancellationToken);

            return response.Message;
        }
    }
}

