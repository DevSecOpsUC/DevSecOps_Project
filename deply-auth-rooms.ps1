# ==========================
# CREACIÓN DE MICROSERVICIOS (AUTH + ROOMS)
# DevSecOps UC - Felipe Barrios
# ==========================

# Variables base
$subscriptionId = "a37b985a-9aa0-4081-8ff3-da0e703daccf"
$resourceGroup  = "rg-devsecopsuc"
$appPlanName    = "asp-devsecopsuc-linux"
$location       = "eastus"

# Imágenes Docker publicadas
$authImage  = "devsecopsuc/auth-service:v11"
$roomsImage = "devsecopsuc/rooms-service:v11"

# Nombres de los WebApps
$authApp  = "auth-service-devsecopsuc"
$roomsApp = "rooms-service-devsecopsuc"

Write-Host "===================================="
Write-Host "Iniciando creación de microservicios DevSecOps UC..."
Write-Host "===================================="

# Establecer suscripción activa
az account set --subscription $subscriptionId

# Validar que el plan existe
$plan = az appservice plan show --name $appPlanName --resource-group $resourceGroup --query "name" -o tsv 2>$null
if (-not $plan) {
    Write-Host "No se encontró el App Service Plan '$appPlanName'." -ForegroundColor Red
    exit 1
}
else {
    Write-Host "App Service Plan encontrado: $appPlanName" -ForegroundColor Green
}

# Crear AUTH SERVICE
Write-Host ""
Write-Host "Creando WebApp: $authApp"
az webapp create `
  --resource-group $resourceGroup `
  --plan $appPlanName `
  --name $authApp `
  --deployment-container-image-name $authImage

# Crear ROOMS SERVICE
Write-Host ""
Write-Host "Creando WebApp: $roomsApp"
az webapp create `
  --resource-group $resourceGroup `
  --plan $appPlanName `
  --name $roomsApp `
  --deployment-container-image-name $roomsImage

# Configurar Docker Hub
Write-Host ""
Write-Host "Configurando origen Docker Hub..."
az webapp config container set `
  --name $authApp `
  --resource-group $resourceGroup `
  --docker-custom-image-name $authImage `
  --docker-registry-server-url https://index.docker.io/v1/

az webapp config container set `
  --name $roomsApp `
  --resource-group $resourceGroup `
  --docker-custom-image-name $roomsImage `
  --docker-registry-server-url https://index.docker.io/v1/

# Reiniciar servicios
Write-Host ""
Write-Host "Reiniciando servicios..."
az webapp restart --name $authApp --resource-group $resourceGroup
az webapp restart --name $roomsApp --resource-group $resourceGroup

# Mostrar URLs finales
Write-Host ""
Write-Host "Servicios desplegados correctamente:" -ForegroundColor Green
Write-Host "   - Auth Service  : https://$authApp.azurewebsites.net/api/auth/login"
Write-Host "   - Rooms Service : https://$roomsApp.azurewebsites.net/api/rooms"
Write-Host ""
Write-Host "Verifica en el portal de Azure que ambos servicios estén 'Running'."
Write-Host "Luego, el frontend podrá consumir estos endpoints mediante HTTPS."
