#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MShare.DbMigrator/MShare.DbMigrator.csproj", "MShare.DbMigrator/"]
RUN dotnet restore "MShare.DbMigrator/MShare.DbMigrator.csproj"
COPY . .
WORKDIR "/src/MShare.DbMigrator"
RUN dotnet build "MShare.DbMigrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MShare.DbMigrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MShare.DbMigrator.dll"]
