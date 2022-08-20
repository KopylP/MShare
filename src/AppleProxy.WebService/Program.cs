using AppleProxy.WebService.Infrastructure.Client;
using Proxy.Api;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("APPLE_PROXY_");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));
builder.Services.AddScoped<IStreamingServiceClient, AppleClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

