terraform {
  backend "azurerm" {
     key                   = "rg-container-registry.tfstate"
     container_name        = "tfstate"
     storage_account_name  = "ozsaccount"
  }
}

resource "azurerm_resource_group" "example" {
  name     = "rg-container-registry"
  location = "East US"
}

resource "azurerm_container_registry" "acr" {
  name                = "containerRegistryoz"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  sku                 = "Basic"
  admin_enabled       = true
}