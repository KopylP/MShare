using System;
using MShare.Songs.Infrastructure;

namespace MShare.Songs.WebApi.Core
{
	public class Bootstrapper
	{
		private readonly WebApplicationBuilder _builder;

		public Bootstrapper(WebApplicationBuilder builder)
		{
			_builder = builder;

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
	}
}

