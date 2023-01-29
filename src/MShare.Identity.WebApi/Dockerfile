#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MShare.Identity.WebApi/MShare.Identity.WebApi.csproj", "MShare.Identity.WebApi/"]
COPY ["MShare.Identity.Infrastructure/MShare.Identity.Infrastructure.csproj", "MShare.Identity.Infrastructure/"]
COPY ["MShare.Identity.Application/MShare.Identity.Application.csproj", "MShare.Identity.Application/"]
COPY ["MShare.Identity.Domain/MShare.Identity.Domain.csproj", "MShare.Identity.Domain/"]
COPY ["MShare.Framework/MShare.Framework.csproj", "MShare.Framework/"]
COPY ["MShare.Identity.Api/MShare.Identity.Api.csproj", "MShare.Identity.Api/"]
COPY ["MShare.Framework.Api/MShare.Framework.Api.csproj", "MShare.Framework.Api/"]
COPY ["MShare.Framework.Types/MShare.Framework.Types.csproj", "MShare.Framework.Types/"]
COPY ["MShare.Framework.Infrastructure/MShare.Framework.Infrastructure.csproj", "MShare.Framework.Infrastructure/"]
COPY ["MShare.Framework.Application/MShare.Framework.Application.csproj", "MShare.Framework.Application/"]
COPY ["MShare.Framework.Domain/MShare.Framework.Domain.csproj", "MShare.Framework.Domain/"]
COPY ["MShare.Framework.WebApi/MShare.Framework.WebApi.csproj", "MShare.Framework.WebApi/"]
RUN dotnet restore "MShare.Identity.WebApi/MShare.Identity.WebApi.csproj"
COPY . .
WORKDIR "/src/MShare.Identity.WebApi"
RUN dotnet build "MShare.Identity.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MShare.Identity.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MShare.Identity.WebApi.dll"]
