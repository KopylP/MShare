using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Actions;
using MShare.Framework.Application.Context;
using MShare.Framework.Infrastructure.Localization;
using MShare.Framework.Infrastructure.Messaging;
using MShare.Framework.Infrastructure.Processing;

namespace MShare.Framework.Infrastructure.Service
{
	public class ServiceModuleBuilder
	{
        private readonly IList<Action<IConfiguration, IServiceCollection>> _actions = new List<Action<IConfiguration, IServiceCollection>>();

        private ServiceModuleBuilder(Action<IConfiguration, IServiceCollection>? initAction = default)
        {
            if (initAction is not null)
                _actions.Add(initAction);
        }

        public static ServiceModuleBuilder Initialize(Action<IConfiguration, IServiceCollection>? initAction = default)
        {
            return new ServiceModuleBuilder(initAction);
        }

        public ServiceModuleBuilder AddMediatR(params Type[] assemblies)
        {
            _actions.Add((_, services) => services.AddMediatR(assemblies));
            return this;
        }

        public ServiceModuleBuilder AddAutoMapper(params Type[] assemblies)
        {
            _actions.Add((_, services) => services.AddAutoMapper(assemblies));
            return this;
        }

        public ServiceModuleBuilder RegisterFilters(params Type[] assemblyMarkers)
        {
            _actions.Add((_, services) =>
            {
                services.RegisterActionHandlers(assemblyMarkers);
                services.AddActionHandlers();
            });
            return this;
        }

        public ServiceModuleBuilder RegisterRequestContexts(params Type[] assemblyMarkers)
        {
            _actions.Add((_, services) => services.RegisterRequestContexts(assemblyMarkers));
            return this;
        }

        public ServiceModuleBuilder AddExecutionContext()
        {
            _actions.Add((_, services) =>
            {
                services.AddHttpContextAccessor();
                services.AddExecutionContext();
            });

            return this;
        }

        public ServiceModuleBuilder AddLocalization()
        {
            _actions.Add((_, services) => services.AddSystemLocalization());
            return this;
        }

        public ServiceModuleMessagingBuilder AddMessaging(Action<MessageOptions> action, params Type[] assemblies)
        {
            return new ServiceModuleMessagingBuilder(this, action, assemblies);
        }

        public ServiceModule Build()
        {
            return new ServiceModule(_actions);
        }

        internal ServiceModuleBuilder AddAction(Action<IConfiguration, IServiceCollection> action)
        {
            _actions.Add(action);
            return this;
        }
    }
}

