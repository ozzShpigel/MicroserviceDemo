
resource "azurerm_servicebus_namespace" "sn" {
  name                = "oz-servicebus-namespace"
  location            = var.location
  resource_group_name = var.resource_group_name
  sku                 = "Basic"

  tags = {
    source = "terraform"
  }
}

resource "azurerm_servicebus_queue" "sq" {
  name         = "CustomerQueue"
  namespace_id = azurerm_servicebus_namespace.sn.id

  enable_partitioning = true
}