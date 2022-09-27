using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MShare.Framework.Infrastructure.Localization
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection AddSystemLocalization(this IServiceCollection services)
        {
			services.AddLocalization();
			return services;
        }
    }
}

