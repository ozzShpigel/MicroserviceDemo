terraform {
  backend "azurerm" {
     key                   = "rg-app-container.tfstate"
     container_name        = "tfstate"
     storage_account_name  = "ozsaccount"
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-app-container"
  location = "East US"
}

module "CUSTOMERAPI-APP" {
  source = "../modules/azure-app-container"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  name = "customerapi-app"
  docker_image = "containerregistryoz.azurecr.io/customer-app"
  docker_image_tag = "latest"
  connection_value = var.customer_connection
  docker_registry_url = "containerregistryoz.azurecr.io"
  docker_registry_username = "containerRegistryoz"
  docker_registry_password = var.docker_registry_password
}

output "CUSTOMERAPI-APP" {
  value = module.CUSTOMERAPI-APP
}

module "ORDERAPI-APP" {
  source = "../modules/azure-app-container"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  name = "orderapi-app"
  docker_image = "containerregistryoz.azurecr.io/order-app"
  docker_image_tag = "latest"
  connection_value = var.order_connection
  docker_registry_url = "containerregistryoz.azurecr.io"
  docker_registry_username = "containerRegistryoz"
  docker_registry_password = var.docker_registry_password
}

output "ORDERAPI-APP" {
  value = module.ORDERAPI-APP
}