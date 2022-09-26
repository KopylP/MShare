using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using MShare.Framework.System.Localization;
using MShare.Songs.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MShare.Songs.WebApi.Core
{
	public class Bootstrapper
	{
		private readonly WebApplicationBuilder _builder;
        private bool _useLocalization = false;

		public Bootstrapper(WebApplicationBuilder builder)
		{
			_builder = builder;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

        }

        public Bootstrapper InitApiVersioning(ApiVersion version)
        {
            _builder.Services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = version;
                o.ReportApiVersions = true;
                o.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            _builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);

            _builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            _builder.Services.AddSwaggerGen();

            return this;
        }

        public Bootstrapper InitLocalization()
        {
            _useLocalization = true;
            _builder.Services.AddSystemLocalization();

            return this;
        }

        public Bootstrapper InitConfiguration()
        {
            _builder.Configuration.AddEnvironmentVariables("MSHARE_SONGS_");
            return this;
        }

		public Bootstrapper InitInfrastructure()
		{
			_builder.Services.AddInfrastructure();

			return this;
		}

		public void Start()
		{
            var app = _builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"../swagger/{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                        options.DefaultModelsExpandDepth(-1);
                        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    }
                });
            }

            //app.UseAuthorization();

            app.MapControllers();

            if (_useLocalization)
                app.UseSystemLocalization();

            // Handles exceptions and generates a custom response body
            app.UseExceptionHandler("/api/v1.0/errors/500");

            // Handles non-success status codes with empty body
            app.UseStatusCodePagesWithReExecute("/api/v1.0/errors/{0}");

            app.Run();
        }
	}
}