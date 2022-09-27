using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Infrastructure.Service;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;
using MShare.Songs.Infrastructure.ProxyService;
using MShare.Songs.Infrastructure.Songs;

namespace MShare.Songs.Infrastructure
{
    using ApplicationAssemblyMarker = Application.IAssemblyMarker;

    public class SongsModule
	{
        public static ServiceModule Service = ServiceModuleBuilder
            .Initialize(Initialize)
            .AddAutoMapper(typeof(ApplicationAssemblyMarker))
            .AddMediatR(typeof(ApplicationAssemblyMarker))
            .RegisterFilters(typeof(ApplicationAssemblyMarker))
            .RegisterRequestContexts(typeof(ApplicationAssemblyMarker))
            .AddExecutionContext()
            .AddLocalization()
			.Build();

		public static void Initialize(IConfiguration configuration, IServiceCollection services)
		{
            services.AddScoped<IStreamingServiceTypeRecognizer, StreamingServiceTypeRecognizer>();
            services.AddScoped<IMediaTypeRecognizer, MediaTypeRecognizer>();
            services.AddScoped<IProxyServiceClientFactory, ProxyServiceClientFactory>();
        }
    }
}

