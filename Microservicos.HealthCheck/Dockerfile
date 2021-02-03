
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

LABEL VERSION="1.0"
LABEL NAME="Healthcheck"
LABEL AUTOR="Leonardo Guilen <leonardoguilen1@gmail.com>"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Microservicos.HealthCheck.csproj", "."]
RUN dotnet restore "Microservicos.HealthCheck.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Microservicos.HealthCheck.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservicos.HealthCheck.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservicos.HealthCheck.dll"]