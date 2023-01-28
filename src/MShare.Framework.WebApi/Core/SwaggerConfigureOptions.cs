using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MShare.Framework.WebApi.Core
{ 
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly SwaggerConfigurationTitle _title;

        public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider, SwaggerConfigurationTitle title)
            => (_provider, _title) = (provider, title);

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = _title.Title,
                    Version = desc.ApiVersion.ToString(),
                });
            }
        }
    }
}

