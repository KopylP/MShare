using MShare.Songs.WebApi.Core;

var builder = WebApplication.CreateBuilder(args);
var bootstrapper = new Bootstrapper(builder);

bootstrapper
    .InitConfiguration()
    .InitInfrastructure()
    .Start();
