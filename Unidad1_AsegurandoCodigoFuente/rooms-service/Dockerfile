# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 🔹 Copiar solo el archivo .csproj del servicio
COPY Unidad1_AsegurandoCodigoFuente/rooms-service/rooms-service.csproj ./rooms-service.csproj
RUN dotnet restore rooms-service.csproj

# 🔹 Copiar el resto del código fuente
COPY Unidad1_AsegurandoCodigoFuente/rooms-service/ ./
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 🔹 Variable para exponer el puerto
ENV ASPNETCORE_URLS=http://+:80

# 🔹 Copiar desde la etapa de build
COPY --from=build /app/publish .

# 🔹 Ejecutar el servicio
ENTRYPOINT ["dotnet", "rooms-service.dll"]
