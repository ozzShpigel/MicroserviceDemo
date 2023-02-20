terraform {
  backend "azurerm" {
     key                   = "rg-demo-function-app-container.tfstate"
     container_name        = "tfstate"
     storage_account_name  = "ozsaccount"
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-oz-function-app"
  location = "West Europe"
}

module "FUNCTION-APP" {
  source = "../modules/azure-function-app"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  name = "function-app-shmok"
  docker_image = "function-app"
  docker_image_tag = "latest"
  docker_registry_url = "containerregistryoz.azurecr.io"
  docker_registry_username = "containerRegistryoz"
  docker_registry_password = var.docker_registry_password
  connection_value = trim(var.order_connection, "\"")
  servicebus_key = var.servicebus_key
}

output "FUNCTION-APP" {
  value = module.FUNCTION-APP
}