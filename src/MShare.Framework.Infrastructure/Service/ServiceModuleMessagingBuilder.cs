using System;
using MShare.Framework.Infrastructure.Messaging;

namespace MShare.Framework.Infrastructure.Service
{
	public class ServiceModuleMessagingBuilder
	{
		public ServiceModuleBuilder ServiceModuleBuilder => _builder;

		private readonly ServiceModuleBuilder _builder;

		internal ServiceModuleMessagingBuilder(ServiceModuleBuilder builder, Action<MessageOptions> action, Type[] assemblies)
		{
			_builder = builder;
			_builder.AddAction((_, services) => services.AddMessaging(action, assemblies));
		}

		public ServiceModuleMessagingBuilder AddIntegrationBus()
		{
			_builder.AddAction((_, services) => services.AddIntegrationBus());
			return this;
		}

		public ServiceModuleMessagingBuilder AddExecutionContext()
		{
			_builder.AddAction((_, services) => services.AddConsumerExecutionContext());
			return this;
		}
	}
}

