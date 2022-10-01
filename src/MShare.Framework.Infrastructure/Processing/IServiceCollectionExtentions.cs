using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MShare.Framework.Infrastructure.Processing
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddActionHandlers(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(BeforeActionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AfterActionBehavior<,>));

            return services;
        }
    }
}

