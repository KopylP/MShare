using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Core;
using MShare.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var apiVersion = new ApiVersion(1, 0);

new Bootstrapper(builder)
    .InitConfiguration("MSHARE_IDENTITY_")
    .InitModule(UserProfilesModule.Service)
    .InitApiVersioning(apiVersion, "Identity API")
    .UseSwagger()
    .MapControllers()
    .UseLocalization()
    .UseErrorPages(apiVersion)
    .MigrateEfDatabase()
    .Start();