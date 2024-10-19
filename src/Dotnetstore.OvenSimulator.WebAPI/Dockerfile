FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Dotnetstore.OvenSimulator.WebAPI/Dotnetstore.OvenSimulator.WebAPI.csproj", "src/Dotnetstore.OvenSimulator.WebAPI/"]
RUN dotnet restore "src/Dotnetstore.OvenSimulator.WebAPI/Dotnetstore.OvenSimulator.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Dotnetstore.OvenSimulator.WebAPI"
RUN dotnet build "Dotnetstore.OvenSimulator.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Dotnetstore.OvenSimulator.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnetstore.OvenSimulator.WebAPI.dll"]
