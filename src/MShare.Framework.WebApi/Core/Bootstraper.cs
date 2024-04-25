using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MShare.Framework.Infrastructure.Service;
using Microsoft.Extensions.Hosting;
using MShare.Framework.Infrastructure.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using LettuceEncrypt;

namespace MShare.Framework.WebApi.Core
{
    public class Bootstrapper
    {
        private readonly List<Action<WebApplication>> _uses = new ();
        private readonly WebApplicationBuilder _builder;

        public Bootstrapper(WebApplicationBuilder builder)
        {
            _builder = builder;

            builder.Services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddEndpointsApiExplorer();
        }

        public Bootstrapper InitApiVersioning(ApiVersion version, string title)
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

            _builder.Services.AddSingleton(new SwaggerConfigurationTitle(title));
            _builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            _builder.Services.AddSwaggerGen();

            return this;
        }

        public Bootstrapper InitConfiguration(string envPrefix)
        {
            _builder.Configuration.AddEnvironmentVariables(envPrefix);
            return this;
        }

        public Bootstrapper InitModule(ServiceModule module)
        {
            module.Register(_builder.Configuration, _builder.Services);
            return this;
        }

        public Bootstrapper AddEncryption()
        {
            _builder.Services.AddLettuceEncrypt();
            return this;
        }

        public Bootstrapper Use(Action<WebApplication> action)
        {
            _uses.Add(action);
            return this;
        }

        public Bootstrapper UseSwagger() => Use(app =>
        {
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
        });

        public Bootstrapper UseLocalization() => Use(app =>
        {
            app.UseSystemLocalization();
        });

        public Bootstrapper MapControllers() => Use(app =>
        {
            app.MapControllers();
        });

        public Bootstrapper UseErrorPages(ApiVersion version) => Use(app =>
        {
            // Handles exceptions and generates a custom response body
            app.UseExceptionHandler($"/api/v{version}/errors/500");

            // Handles non-success status codes with empty body
            app.UseStatusCodePagesWithReExecute($"/api/v{version}/errors/{{0}}");
        });

        public Bootstrapper MigrateEfDatabase() => Use(app =>
        {
            if (app.Configuration.GetValue<bool>("DatabaseAutoMigrationsEnabled", false))
            {
                using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DbContext>();
                    context?.Database.Migrate();
                }
            }
        });

        public Bootstrapper UseHttpsRedirection() => Use(app => app.UseHttpsRedirection());

        public void Start()
        {
            var app = _builder.Build();

            _uses.ForEach(action => action(app));

            app.Run();
        }
    }
}

