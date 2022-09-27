using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MShare.Framework.Infrastructure.Service
{
	public class ServiceModule
	{
        private readonly IEnumerable<Action<IConfiguration, IServiceCollection>> _actions;

        internal ServiceModule(IEnumerable<Action<IConfiguration, IServiceCollection>> actions)
        {
            _actions = actions;
        }

        public void Register(IConfiguration configuration, IServiceCollection services)
        {
            foreach (var action in _actions)
                action(configuration, services);
        }
    }
}

