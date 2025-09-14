# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY CoinKeep.sln .
COPY CoinKeep.Api/ CoinKeep.Api/
COPY CoinKeep.Core/ CoinKeep.Core/
COPY CoinKeep.Infrastructure/ CoinKeep.Infrastructure/

RUN dotnet restore CoinKeep.Api/CoinKeep.Api.csproj
RUN dotnet publish CoinKeep.Api/CoinKeep.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CoinKeep.Api.dll"]
