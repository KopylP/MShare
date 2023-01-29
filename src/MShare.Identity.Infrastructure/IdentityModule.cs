using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Infrastructure.Service;
using MShare.Identity.Infrastructure.Persistence;
using MShare.Framework.Infrastructure.Persistance;
using MShare.Framework.Infrastructure.SqlClient;

namespace MShare.Identity.Infrastructure
{
    using ApplicationAssemblyMarker = Application.IAssemblyMarker;

    public class UserProfilesModule
    {
            public static ServiceModule Service = ServiceModuleBuilder
                .Initialize(Initialize)
                .AddAutoMapper(typeof(ApplicationAssemblyMarker))
                .AddMediatR(typeof(ApplicationAssemblyMarker))
                .RegisterFilters(typeof(ApplicationAssemblyMarker))
                .RegisterRequestContexts(typeof(ApplicationAssemblyMarker))
                .AddMessaging(opt => opt.SelfUri = "identity", typeof(ApplicationAssemblyMarker))
                    .AddIntegrationBus()
                    .ServiceModuleBuilder
                .AddPostgres<ApplicationContext>()
                .AddPostgresSqlClient()
                .Build();

            public static void Initialize(IConfiguration configuration, IServiceCollection services)
            {
                RegisterRepositories(services);
            }

            private static void RegisterRepositories(IServiceCollection services)
            {

            }
        }
}

