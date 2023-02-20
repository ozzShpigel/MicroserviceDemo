terraform {
  backend "azurerm" {
     key                   = "rg-servicebus-container.tfstate"
     container_name        = "tfstate"
     storage_account_name  = "ozsaccount"
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-servicebus-container"
  location = "East US"
}

module "SERVICEBUS" {
  source = "../modules/azure-servicebus-queue"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
}

output "SERVICEBUS" {
  value = module.SERVICEBUS
}