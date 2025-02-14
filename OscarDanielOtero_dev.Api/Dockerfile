FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OscarDanielOtero_dev.Api/OscarDanielOtero_dev.Api.csproj", "OscarDanielOtero_dev.Api/"]
COPY ["BusinessLogicServices/BusinessLogicServices.csproj", "BusinessLogicServices/"]
COPY ["FirestoreInfrastructureServices/FirestoreInfrastructureServices.csproj", "FirestoreInfrastructureServices/"]
COPY ["Models/Models.csproj", "Models/"]
RUN dotnet restore "OscarDanielOtero_dev.Api/OscarDanielOtero_dev.Api.csproj"
COPY . .
WORKDIR "/src/OscarDanielOtero_dev.Api"
RUN dotnet build "OscarDanielOtero_dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OscarDanielOtero_dev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OscarDanielOtero_dev.Api.dll"]
