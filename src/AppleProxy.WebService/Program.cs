using AppleProxy.WebService.Infrastructure.Client;
using MShare.Framework.Infrastructure.AccessToken;
using MShare.Framework.Infrastructure.AccessToken.Factories;
using MShare.Framework.Infrastructure.AccessToken.Factories.Apple;
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
builder.Services.AddAccessTokenStore();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
builder.Services.AddScoped<ITokenFactory, TokenFactory>();

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

