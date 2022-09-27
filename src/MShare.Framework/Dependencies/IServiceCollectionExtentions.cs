using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MShare.Framework.Dependencies
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection RegisterGenericForAssemblies(this IServiceCollection services, Type genericInterfaceType, params Type[] assembliesTypes)
        {
            foreach (var assemblyType in assembliesTypes)
                InitForAssembly(services, genericInterfaceType, assemblyType.Assembly);

            return services;
        }

        private static void InitForAssembly(IServiceCollection services, Type genericInterfaceType, Assembly assembly)
        {
            var implementations = assembly.GetTypes().Where(x => !x.IsInterface && x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == genericInterfaceType));
            foreach (var implementation in implementations)
                Register(services, implementation, genericInterfaceType);
        }

        private static void Register(IServiceCollection services, Type implementation, Type genericInterfaceType)
        {
            var @interface = implementation.GetInterfaces().SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
            services.AddScoped(@interface, implementation);
        }
    }
}

