using System;
using MShare.Framework.Api;
using MShare.Framework.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection AddExecutionContext(this IServiceCollection services)
		{
			services.AddTransient<IExecutionContext, MShare.Framework.Infrastructure.ExecutionContext>();
            services.AddTransient<IContextExecutor, HttpContextExecutor>();

            return services;
		}
	}
}

