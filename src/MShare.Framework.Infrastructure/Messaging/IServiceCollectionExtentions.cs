using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Api;
using MShare.Framework.Application;
using Polly;

namespace MShare.Framework.Infrastructure.Messaging
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection AddMessaging(this IServiceCollection services, Action<MessageOptions> action, params Type[] assemblies)
		{
			var options = new MessageOptions();
			action?.Invoke(options);
			services.AddSingleton<IMessageOptions>(options);
			assemblies ??= Array.Empty<Type>();

			services.AddMassTransit((opt) =>
			{
                opt.SetKebabCaseEndpointNameFormatter();
                opt.UsingInMemory((context, cfg) =>
				{
                    cfg.ReceiveEndpoint(options.SelfUri, conf =>
					{
                    });

					cfg.ConfigureEndpoints(context);

                });
                opt.AddConsumers(assemblies.Select(p => p.Assembly).ToArray());
            });

			return services;
		}

		public static IServiceCollection AddIntegrationBus(this IServiceCollection services)
		{
			services.AddTransient<IIntegrationBus, IntegrationBus>();
			return services;
		}

		public static IServiceCollection AddConsumerExecutionContext(this IServiceCollection services)
		{
			services.AddTransient<IConsumerContextExecutor, ConsumerContextExecutor>();
			return services;
		}
	}
}

