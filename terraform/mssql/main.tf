terraform {
  backend "azurerm" {
     key                   = "rg-mssql-container.tfstate"
     container_name        = "tfstate"
     storage_account_name  = "ozsaccount"
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-mssql-container"
  location = "East US"
}

module "CUSTOMERAPI-MSSQL" {
  source = "../modules/azure-mssql-container"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  continst_name = "customerapi"
  name = "customerapi-mssql"
  label = "customer-mssql"
}

output "CUSTOMERAPI-MSSQL" {
  value = module.CUSTOMERAPI-MSSQL
}

module "ORDERAPI-MSSQL" {
  source = "../modules/azure-mssql-container"
  location = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  continst_name = "orderapi"
  name = "orderapi-mssql"
  label = "order-mssql"
}

output "ORDERAPI-MSSQL" {
  value = module.ORDERAPI-MSSQL
}