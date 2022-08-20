#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AppleProxy.WebService/AppleProxy.WebService.csproj", "AppleProxy.WebService/"]
COPY ["Proxy.Api/Proxy.Api.csproj", "Proxy.Api/"]
COPY ["MShare.Framework.WebApi/MShare.Framework.WebApi.csproj", "MShare.Framework.WebApi/"]
RUN dotnet restore "AppleProxy.WebService/AppleProxy.WebService.csproj"
COPY . .
WORKDIR "/src/AppleProxy.WebService"
RUN dotnet build "AppleProxy.WebService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AppleProxy.WebService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppleProxy.WebService.dll"]
