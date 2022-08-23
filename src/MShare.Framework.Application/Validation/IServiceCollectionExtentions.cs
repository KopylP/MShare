using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Types.Extentions;

namespace MShare.Framework.Application.Validation
{
	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection RegisterValidators(this IServiceCollection services, params Type[] assembliesTypes)
		{
            foreach (var assemblyType in assembliesTypes)
                InitValidatorsForAssembly(services, assemblyType.Assembly);

            return services;
		}

        private static void InitValidatorsForAssembly(IServiceCollection services, Assembly assembly)
        {
            var validators = assembly.GetTypes().Where(ValidatorsSpecification.Instance);
            foreach (var validator in validators)
                RegisterValidator(services, validator);
        }

        private static void RegisterValidator(IServiceCollection services, Type handler)
        {
            var interfaceType = handler.GetInterfaces().SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRequestValidator<,>));
            services.AddScoped(interfaceType, handler);
        }
    }
}

