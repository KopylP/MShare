#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MShare.Songs.WebApi/MShare.Songs.WebApi.csproj", "MShare.Songs.WebApi/"]
COPY ["MShare.Songs.Infrastructure/MShare.Songs.Infrastructure.csproj", "MShare.Songs.Infrastructure/"]
COPY ["MShare.Songs.Application/MShare.Songs.Application.csproj", "MShare.Songs.Application/"]
COPY ["MShare.Framework.Application/MShare.Framework.Application.csproj", "MShare.Framework.Application/"]
COPY ["MShare.Framework.Api/MShare.Framework.Api.csproj", "MShare.Framework.Api/"]
COPY ["MShare.Framework.Types/MShare.Framework.Types.csproj", "MShare.Framework.Types/"]
COPY ["MShare.Songs.Api/MShare.Songs.Api.csproj", "MShare.Songs.Api/"]
COPY ["MShare.Songs.Abstractions/MShare.Songs.Abstractions.csproj", "MShare.Songs.Abstractions/"]
COPY ["MShare.Framework.Domain/MShare.Framework.Domain.csproj", "MShare.Framework.Domain/"]
COPY ["MShare.Framework/MShare.Framework.csproj", "MShare.Framework/"]
COPY ["MShare.Songs.Domain/MShare.Songs.Domain.csproj", "MShare.Songs.Domain/"]
COPY ["MShare.Framework.WebApi/MShare.Framework.WebApi.csproj", "MShare.Framework.WebApi/"]
COPY ["MShare.Framework.Infrastructure/MShare.Framework.Infrastructure.csproj", "MShare.Framework.Infrastructure/"]
COPY ["ProxyService.Client/ProxyService.Client.csproj", "ProxyService.Client/"]
RUN dotnet restore "MShare.Songs.WebApi/MShare.Songs.WebApi.csproj"
COPY . .
WORKDIR "/src/MShare.Songs.WebApi"
RUN dotnet build "MShare.Songs.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MShare.Songs.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sleep", "60", "&&", "dotnet", "MShare.Songs.WebApi.dll"]
