using System;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
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

