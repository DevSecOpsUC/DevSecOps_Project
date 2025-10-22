@description('Nombre del grupo de recursos donde se desplegará la aplicación.')
param resourceGroupName string = 'rg-devsecopsuc'

@description('Nombre de la aplicación web.')
param webAppName string = 'devsecopsuc-webapp'

@description('Ubicación del despliegue.')
param location string = resourceGroup().location

@description('Nombre del plan de App Service.')
param appServicePlanName string = 'asp-devsecopsuc'

@description('Tipo de plan de servicio (por defecto, gratuito para pruebas).')
param skuName string = 'F1'

@description('Nivel de seguridad para las configuraciones por defecto.')
param httpsOnly bool = true

// 🔐 App Service Plan (Linux)
resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: skuName
    capacity: 1
  }
  kind: 'linux'
  properties: {
    reserved: true // necesario para planes linux
  }
}

// 🌐 Web App para contenedores Linux
resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: webAppName
  location: location
  kind: 'app,linux,container'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: httpsOnly
    siteConfig: {
      // linuxFxVersion se puede dejar vacío, lo definiremos desde GitHub Actions
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
    }
  }
  dependsOn: [
    appServicePlan
  ]
}

// 📤 Salidas
output webAppUrl string = 'https://${webAppName}.azurewebsites.net'
output appServicePlanId string = appServicePlan.id
