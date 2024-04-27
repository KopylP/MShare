using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MShare.Framework.WebApi.Core;
using MShare.Songs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var apiVersion = new ApiVersion(1, 0);

new Bootstrapper(builder)
    .InitConfiguration("MSHARE_SONGS_")
    .InitModule(SongsModule.Service)
    .InitApiVersioning(apiVersion, "Songs API")
    .AddEncryption()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseSwagger()
    .MapControllers()
    .UseLocalization()
    .UseErrorPages(apiVersion)
    .MigrateEfDatabase()
    .Start();