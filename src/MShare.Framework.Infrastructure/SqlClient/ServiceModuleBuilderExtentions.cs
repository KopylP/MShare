using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.SqlClient;
using MShare.Framework.Infrastructure.Service;

namespace MShare.Framework.Infrastructure.SqlClient
{
	public static class ServiceModuleBuilderExtentions
	{
        public static ServiceModuleBuilder AddPostgresSqlClient(this ServiceModuleBuilder builder, string connectionName = "DefaultConnection")
        {
            builder.AddAction((configuration, services) =>
            {
                var connection = configuration.GetConnectionString(connectionName);
                services.AddScoped<IDbConnectionFactory>(services => new PostgresDbConnectionFactory(connection));
                services.AddScoped<ISqlQueryExecutor, SqlQueryExecutor>();
            });

            return builder;
        }
    }
}