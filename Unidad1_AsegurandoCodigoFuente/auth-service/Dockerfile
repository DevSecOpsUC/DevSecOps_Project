# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo el .csproj desde la raíz
COPY Unidad1_AsegurandoCodigoFuente/auth-service/auth-service.csproj ./auth-service.csproj
RUN dotnet restore auth-service.csproj

# Copiar solo la carpeta del servicio
COPY Unidad1_AsegurandoCodigoFuente/auth-service/ ./

# Compilar y publicar
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "auth-service.dll"]
