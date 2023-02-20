resource "azurerm_service_plan" "sp" {
  name                = "app-service-plan"
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_function_app" "fa" {
  name                = var.name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.sp.id
  storage_account_name = "ozsaccount"

  site_config {

  application_stack {
    docker{
      registry_url = var.docker_registry_url
      image_name = var.docker_image
      image_tag = var.docker_image_tag
      registry_username = var.docker_registry_username
      registry_password = var.docker_registry_password
      }
    }
  }
  
  app_settings = {
    "QueueConnectionString" = var.servicebus_key
    "DatabaseConnectionString" = var.connection_value
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }
}