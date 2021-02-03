
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

LABEL VERSION="1.0"
LABEL NAME="API Gateway"
LABEL AUTOR="Leonardo Guilen <leonardoguilen1@gmail.com>"
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Microservicos.APIGateway.csproj", "."]
RUN dotnet restore "Microservicos.APIGateway.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Microservicos.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservicos.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservicos.APIGateway.dll"]