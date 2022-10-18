using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Infrastructure.Persistance;
using MShare.Framework.Infrastructure.Service;
using MShare.Framework.Infrastructure.SqlClient;
using MShare.Songs.Api.Messages;
using MShare.Songs.Application.Factories;
using MShare.Songs.Domain;
using MShare.Songs.Infrastructure.Persistence;
using MShare.Songs.Infrastructure.Persistence.Repositories;
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
            .AddMessaging(opt => opt.SelfUri = "songs", typeof(ApplicationAssemblyMarker), typeof(UnsavedAlbumRequestedEvent))
                .AddIntegrationBus()
                .AddExecutionContext()
                .ServiceModuleBuilder
            .AddPostgres<ApplicationContext>()
            .AddPostgresSqlClient()
            .Build();

		public static void Initialize(IConfiguration configuration, IServiceCollection services)
		{
            RegisterRepositories(services);

            services.AddScoped<IStreamingServiceTypeRecognizer, StreamingServiceTypeRecognizer>();
            services.AddScoped<IMediaTypeRecognizer, MediaTypeRecognizer>();
            services.AddScoped<IProxyServiceClientFactory, ProxyServiceClientFactory>();
            services.AddScoped<IMetadataExtractor, IdExtractor>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
        }
    }
}