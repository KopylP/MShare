using Microsoft.AspNetCore.Mvc;
using MShare.Songs.WebApi.Core;

var builder = WebApplication.CreateBuilder(args);
var bootstrapper = new Bootstrapper(builder);

bootstrapper
    .InitConfiguration()
    .InitService()
    .InitApiVersioning(new ApiVersion(1, 0))
    .Start();
