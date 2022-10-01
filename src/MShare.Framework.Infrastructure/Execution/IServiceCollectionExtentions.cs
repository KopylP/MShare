using System;
using MShare.Framework.Api;
using MShare.Framework.Application.Context;
using MShare.Framework.Infrastructure;
using MShare.Framework.Infrastructure.Execution;
using ExecutionContext = MShare.Framework.Infrastructure.Execution.ExecutionContext;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection AddExecutionContext(this IServiceCollection services)
		{
			services.AddTransient<IExecutionContext, ExecutionContext>();
            services.AddTransient<IHttpContextExecutor, HttpContextExecutor>();
			services.AddTransient<IExecutionContextUpdater, ContextUpdater>();

            return services;
		}
	}
}

