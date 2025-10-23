@description('Nombre del grupo de recursos')
param resourceGroupName string = 'rg-devsecopsuc'

@description('Ubicación de los recursos')
param location string = 'eastus'

@description('Nombre del App Service Plan')
param appServicePlanName string = 'asp-devsecopsuc-linux'

@description('Nombre de la aplicación web')
param webAppName string = 'webapp-devsecopsuc'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'B1' // Plan básico, puedes subirlo a S1 si quieres más rendimiento
    tier: 'Basic'
    capacity: 1
  }
  properties: {
    reserved: true // <-- Muy importante: esto indica que es Linux
  }
}

resource webApp 'Microsoft.Web/sites@2022-09-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'NGINX|latest' // Usa NGINX como imagen base
      alwaysOn: true
      appSettings: [
        {
          name: 'WEBSITES_ENABLE_APP_SERVICE_STORAGE'
          value: 'true'
        }
        {
          name: 'WEBSITES_PORT'
          value: '80'
        }
      ]
    }
  }
  dependsOn: [
    appServicePlan
  ]
}

output webAppUrl string = 'https://${webApp.name}.azurewebsites.net'
