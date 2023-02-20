

resource "azurerm_container_group" "cg" {
  name                = var.continst_name
  location            = var.location
  resource_group_name = var.resource_group_name
  ip_address_type     = "Public"
  dns_name_label      = var.label
  os_type             = "Linux"

  container {
    name   = var.name
    image  = "mcr.microsoft.com/mssql/server:2019-latest"
    cpu    = "1"
    memory = "2"
    environment_variables = {
      ACCEPT_EULA="Y"
      MSSQL_SA_PASSWORD="Qwe12345"
      MSSQL_PID="Express"
    }

    ports {
      port     = 1433
      protocol = "TCP"
    }
  }

  tags = {
    environment = "testing"
  }
}