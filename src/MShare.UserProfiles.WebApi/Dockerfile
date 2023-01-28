#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MShare.UserProfiles.WebApi/MShare.UserProfiles.WebApi.csproj", "MShare.UserProfiles.WebApi/"]
COPY ["MShare.Framework.WebApi/MShare.Framework.WebApi.csproj", "MShare.Framework.WebApi/"]
COPY ["MShare.Framework.Infrastructure/MShare.Framework.Infrastructure.csproj", "MShare.Framework.Infrastructure/"]
COPY ["MShare.Framework.Api/MShare.Framework.Api.csproj", "MShare.Framework.Api/"]
COPY ["MShare.Framework.Types/MShare.Framework.Types.csproj", "MShare.Framework.Types/"]
COPY ["MShare.Framework/MShare.Framework.csproj", "MShare.Framework/"]
COPY ["MShare.Framework.Application/MShare.Framework.Application.csproj", "MShare.Framework.Application/"]
COPY ["MShare.Framework.Domain/MShare.Framework.Domain.csproj", "MShare.Framework.Domain/"]
COPY ["MShare.UserProfiles.Infrastructure/MShare.UserProfiles.Infrastructure.csproj", "MShare.UserProfiles.Infrastructure/"]
COPY ["MShare.Framework.System/MShare.Framework.System.csproj", "MShare.Framework.System/"]
COPY ["MShare.UserProfiles.Application/MShare.UserProfiles.Application.csproj", "MShare.UserProfiles.Application/"]
RUN dotnet restore "MShare.UserProfiles.WebApi/MShare.UserProfiles.WebApi.csproj"
COPY . .
WORKDIR "/src/MShare.UserProfiles.WebApi"
RUN dotnet build "MShare.UserProfiles.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MShare.UserProfiles.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MShare.UserProfiles.WebApi.dll"]
