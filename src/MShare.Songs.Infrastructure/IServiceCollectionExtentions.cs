using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Validation;
using MShare.Framework.Infrastructure.Processing;
using MShare.Framework.Types.Extentions;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;
using MShare.Songs.Infrastructure.ProxyService;
using MShare.Songs.Infrastructure.Songs;

namespace MShare.Songs.Infrastructure
{
    using ApplicationAssemblyMarker = Application.IAssemblyMarker;

	public static class IServiceCollectionExtentions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
            services
                .AddMediatR(typeof(ApplicationAssemblyMarker))
                .AddAutoMapper(typeof(ApplicationAssemblyMarker))
                .RegisterValidators(typeof(ApplicationAssemblyMarker))
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddRequestValidation()
                .AddExecutionContext();

            services.AddScoped<IStreamingServiceTypeRecognizer, StreamingServiceTypeRecognizer>();
            services.AddScoped<IProxyServiceClientFactory, ProxyServiceClientFactory>();

            return services;
		}
    }
}

