
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

LABEL VERSION="1.0"
LABEL NAME="Authentication API"
LABEL AUTOR="Leonardo Guilen <leonardoguilen1@gmail.com>"

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Microservicos.AuthenticationService.csproj", "."]
RUN dotnet restore "Microservicos.AuthenticationService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Microservicos.AuthenticationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservicos.AuthenticationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservicos.AuthenticationService.dll"]