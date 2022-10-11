using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Domain;
using MShare.Framework.Infrastructure.Persistance.EntityFramework;
using MShare.Framework.Infrastructure.Service;

namespace MShare.Framework.Infrastructure.Persistance
{
	public static class ServiceModuleBuilderExtentions
	{
        public static ServiceModuleBuilder AddPostgres<Context>(this ServiceModuleBuilder builder, string connectionName = "DefaultConnection")
            where Context : DbContext
        {
            builder.AddAction((conf, services) =>
            {
                var connection = conf.GetConnectionString(connectionName);
                services.AddDbContext<DbContext, Context>(options => options.UseNpgsql(connection));
                services.AddScoped<IUnitOfWorkProvider, UnitOfWorkProvider>();
            });

            return builder;
        }
    }
}