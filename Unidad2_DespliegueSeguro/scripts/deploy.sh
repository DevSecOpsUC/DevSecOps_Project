#!/bin/bash
set -e

echo "🔹 Creando grupo de recursos..."
az group create -n rg-devsecopsuc -l eastus || true

echo "🔹 Desplegando infraestructura con Bicep..."
az deployment group create \
  -g rg-devsecopsuc \
  --template-file ./Unidad2_DespliegueYOperacionesSeguras/bicep/main.bicep \
  --parameters webAppName=devsecopsuc-webapp appServicePlanName=asp-devsecopsuc

echo "🔹 Iniciando sesión en Azure Container Registry..."
az acr login --name devsecopsucacr

echo "🔹 Construyendo y enviando imágenes..."
docker build -t devsecopsucacr.azurecr.io/auth-service:latest -f ./Unidad2_DespliegueYOperacionesSeguras/docker/Dockerfile.auth .
docker push devsecopsucacr.azurecr.io/auth-service:latest

docker build -t devsecopsucacr.azurecr.io/rooms-service:latest -f ./Unidad2_DespliegueYOperacionesSeguras/docker/Dockerfile.rooms .
docker push devsecopsucacr.azurecr.io/rooms-service:latest

echo "🔹 Configurando contenedores en Azure Web App..."
az webapp config container set \
  --name devsecopsuc-webapp \
  --resource-group rg-devsecopsuc \
  --multicontainer-config-type compose \
  --multicontainer-config-file ./Unidad2_DespliegueYOperacionesSeguras/docker/docker-compose.yml

echo "✅ Despliegue completado correctamente."
