
resource "azurerm_service_plan" "sp" {
  name                = "app-service-plan"
  location            = var.location
  resource_group_name = var.resource_group_name
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "wa" {
  name                = var.name
  resource_group_name = var.resource_group_name
  location            = var.location
  service_plan_id     = azurerm_service_plan.sp.id

  site_config {

  application_stack {
    docker_image = var.docker_image
    docker_image_tag = var.docker_image_tag
    dotnet_version = "6.0"
    }
  }

  app_settings = {
    "DOCKER_REGISTRY_SERVER_URL"          = var.docker_registry_url
    "DOCKER_REGISTRY_SERVER_USERNAME"     = var.docker_registry_username
    "DOCKER_REGISTRY_SERVER_PASSWORD"     = var.docker_registry_password
    "WEBSITES_ENABLE_APP_SERVICE_STORAGE" = "false"
  }

  connection_string {
    name = "CustomerDatabase"
    type = "SQLServer"
    value = var.connection_value
  }
}