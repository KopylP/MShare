using System;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Dependencies;

namespace MShare.Framework.Application.Validation
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection RegisterValidators(this IServiceCollection services, params Type[] assembliesTypes)
		{
            services.RegisterGenericForAssemblies(typeof(IRequestValidator<,>), assembliesTypes);

            return services;
		}
    }
}

