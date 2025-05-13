variable "resource_group_location" {
  type        = string
  description = "Location for all resources."
  default     = "Brazil South"
}

variable "resource_group_name" {
  type        = string
  description = "Prefix of the resource group name in your Azure subscription."
  default     = "fiap-pos-tech-database-rg"
}

variable "mssql_server_name" {
  type        = string
  description = "The name of the SQL Database Server."
  default     = "sql-tech-challenge-server"
}

variable "sql_db_name" {
  type        = string
  description = "The name of the SQL Database."
  default     = "tech-challenge-revenda-de-veiculos"
}

variable "admin_username" {
  type        = string
  description = "The administrator username of the SQL logical server."
  default     = "azureadmin"
}

variable "admin_password" {
  type        = string
  description = "The administrator password of the SQL logical server."
  sensitive   = true
  default     = "{{ secrets.SA_PASSWORD }}"
}