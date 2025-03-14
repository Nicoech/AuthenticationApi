# Etapa base con ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5001


# Etapa de build con el SDK de .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Authentication.Api/Authentication.Api.csproj", "Authentication.Api/"]
COPY ["Authentication.Infrastructure/Authentication.Infrastructure.csproj", "Authentication.Infrastructure/"]
COPY ["Authentication.Domain/Authentication.Domain.csproj", "Authentication.Domain/"]
RUN dotnet restore "Authentication.Api/Authentication.Api.csproj"

# Instalar la herramienta dotnet-ef
RUN dotnet tool install -g dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar todo el código al contenedor
COPY . . 

# Compilar la aplicación
WORKDIR "/src/Authentication.Api"
RUN dotnet build "Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final donde se copian los archivos ya publicados
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa de publicación
COPY --from=publish /app/publish .
# Establecer el entrypoint para ejecutar las migraciones y luego la aplicación
ENTRYPOINT ["dotnet", "Authentication.Api.dll", "database", "update"]
