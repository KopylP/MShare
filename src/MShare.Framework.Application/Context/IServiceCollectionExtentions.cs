using System;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Dependencies;

namespace MShare.Framework.Application.Context
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection RegisterRequestContexts(this IServiceCollection services, params Type[] assembliesTypes)
        {
            services.RegisterGenericForAssemblies(typeof(IQueryContext<, >), assembliesTypes);

            return services;
        }
    }
}

