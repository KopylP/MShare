#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SpotifyProxy.WebService/SpotifyProxy.WebService.csproj", "SpotifyProxy.WebService/"]
COPY ["Proxy.Api/Proxy.Api.csproj", "Proxy.Api/"]
COPY ["MShare.Framework.Infrastructure/MShare.Framework.Infrastructure.csproj", "MShare.Framework.Infrastructure/"]
COPY ["MShare.Framework.Types/MShare.Framework.Types.csproj", "MShare.Framework.Types/"]
COPY ["MShare.Framework.WebApi/MShare.Framework.WebApi.csproj", "MShare.Framework.WebApi/"]
COPY ["MShare.Framework/MShare.Framework.csproj", "MShare.Framework/"]
RUN dotnet restore "SpotifyProxy.WebService/SpotifyProxy.WebService.csproj"
COPY . .
WORKDIR "/src/SpotifyProxy.WebService"
RUN dotnet build "SpotifyProxy.WebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SpotifyProxy.WebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotifyProxy.WebService.dll"]
