using System;
using Microsoft.Extensions.DependencyInjection;

namespace MShare.Framework.Infrastructure.AccessToken
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddAccessTokenStore(this IServiceCollection services)
        {
            services.AddScoped<IAccessTokenStore, AccessTokenStore>();

            return services;
        }
    }
}

