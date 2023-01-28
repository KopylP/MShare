using Microsoft.AspNetCore.Mvc;
using MShare.Framework.WebApi.Core;
using MShare.UserProfiles.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var apiVersion = new ApiVersion(1, 0);

new Bootstrapper(builder)
    .InitConfiguration("MSHARE_USER_PROFILES_")
    .InitModule(UserProfilesModule.Service)
    .InitApiVersioning(apiVersion, "User Profiles API")
    .UseSwagger()
    .MapControllers()
    .UseLocalization()
    .UseErrorPages(apiVersion)
    .MigrateEfDatabase()
    .Start();