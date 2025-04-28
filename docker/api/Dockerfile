FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Sync.Api/Sync.Api.csproj", "src/Sync.Api/"]
COPY ["src/Sync.Common/Sync.Common.csproj", "src/Sync.Common/"]
RUN dotnet restore "src/Sync.Api/Sync.Api.csproj"
COPY . .
WORKDIR "/src/src/Sync.Api"
RUN dotnet build "Sync.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sync.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sync.Api.dll"]