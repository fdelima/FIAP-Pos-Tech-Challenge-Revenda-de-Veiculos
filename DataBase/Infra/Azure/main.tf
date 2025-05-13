# https://learn.microsoft.com/pt-br/azure/azure-sql/database/single-database-create-terraform-quickstart?view=azuresql&tabs=azure-cli
# terraform init -upgrade
# terraform plan -out main.tfplan
# terraform apply main.tfplan
# terraform plan -destroy -out main.destroy.tfplan
# terraform apply main.destroy.tfplan

resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.resource_group_location
}

resource "azurerm_mssql_server" "server" {
  name                         = var.mssql_server_name
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  administrator_login          = var.admin_username
  administrator_login_password = var.admin_password
  version                      = "12.0"
}

resource "azurerm_mssql_database" "db" {
  name      = var.sql_db_name
  server_id = azurerm_mssql_server.server.id
}