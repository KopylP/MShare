using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MShare.Framework.Infrastructure.Localization;
using MShare.Songs.Infrastructure;
using MShare.Songs.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace MShare.Songs.WebApi.Core
{
	public class Bootstrapper
	{
		private readonly WebApplicationBuilder _builder;

		public Bootstrapper(WebApplicationBuilder builder)
		{
			_builder = builder;

            builder.Services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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

        public Bootstrapper InitConfiguration()
        {
            _builder.Configuration.AddEnvironmentVariables("MSHARE_SONGS_");
            return this;
        }

		public Bootstrapper InitService()
		{
            SongsModule.Service.Register(_builder.Configuration, _builder.Services);
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

            app.UseSystemLocalization();

            // Handles exceptions and generates a custom response body
            app.UseExceptionHandler("/api/v1.0/errors/500");

            // Handles non-success status codes with empty body
            app.UseStatusCodePagesWithReExecute("/api/v1.0/errors/{0}");

            MigrateDatabase(app);

            app.Run();
        }

        private void MigrateDatabase(WebApplication app)
        {
            if (app.Configuration.GetValue<bool>("DatabaseAutoMigrationsEnabled", false))
            {
                using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DbContext>();
                    context?.Database.Migrate();
                }
            }
        }
	}
}