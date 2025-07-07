# Fase 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el .sln y los .csproj para restaurar dependencias
COPY ["ProyectoCentinela.sln", "."]
COPY ["ProyectoCentinela.WebApp/ProyectoCentinela.WebApp.csproj", "ProyectoCentinela.WebApp/"]
RUN dotnet restore "ProyectoCentinela.sln"

# Copia el resto del código fuente
COPY . .
WORKDIR "/src/ProyectoCentinela.WebApp"
RUN dotnet build "ProyectoCentinela.WebApp.csproj" -c Release -o /app/build

# Fase 2: Publish
FROM build AS publish
RUN dotnet publish "ProyectoCentinela.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Fase 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ProyectoCentinela.WebApp.dll"]
