#!/bin/bash
# Configura conexión de App Service a Log Analytics y activa Sentinel

RESOURCE_GROUP="rg-devsecopsuc"
SUBSCRIPTION_ID="a37b985a-9aa0-4081-8ff3-da0e703daccf"
WORKSPACE_NAME="DevSecOpsUCLogs"
WEBAPP_NAME="webapp-devsecopsuc"
LOCATION="eastus"

echo "🔹 Creando workspace Log Analytics..."
az monitor log-analytics workspace create \
  --resource-group $RESOURCE_GROUP \
  --workspace-name $WORKSPACE_NAME \
  --location $LOCATION

echo "🔹 Vinculando WebApp al workspace..."
az monitor diagnostic-settings create \
  --name "SendToSentinel" \
  --resource "/subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP/providers/Microsoft.Web/sites/$WEBAPP_NAME" \
  --workspace "/subscriptions/$SUBSCRIPTION_ID/resourceGroups/$RESOURCE_GROUP/providers/Microsoft.OperationalInsights/workspaces/$WORKSPACE_NAME" \
  --logs '[{"category":"AppServiceHTTPLogs","enabled":true},{"category":"AppServiceConsoleLogs","enabled":true}]'

echo "🔹 Activando Microsoft Sentinel..."
az sentinel create \
  --resource-group $RESOURCE_GROUP \
  --workspace-name $WORKSPACE_NAME

echo "✅ Configuración completada. Abre Sentinel en el portal para visualizar eventos."
