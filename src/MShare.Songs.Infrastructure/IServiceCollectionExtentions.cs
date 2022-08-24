using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Application.Validation;
using MShare.Framework.Infrastructure.Processing;
using MShare.Framework.Types.Extentions;
using MShare.Songs.Domain;
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
                .RegisterValidators(typeof(ApplicationAssemblyMarker))
                .AddRequestValidation();

            services.AddScoped<IStreamingServiceTypeRecognizer, StreamingServiceTypeRecognizer>();

            return services;
		}
    }
}

