using System;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Dependencies;

namespace MShare.Framework.Application.Actions
{
	public static class IServiceCollectionExtentions
	{
        public static IServiceCollection RegisterActionHandlers(this IServiceCollection services, params Type[] assembliesTypes)
        {
            services.RegisterGenericForAssemblies(typeof(IBeforeActionHandler<,>), assembliesTypes);
            services.RegisterGenericForAssemblies(typeof(IRequestValidator<,>), assembliesTypes);
            services.RegisterGenericForAssemblies(typeof(IAfterActionHandler<,>), assembliesTypes);

            return services;
        }
    }
}

