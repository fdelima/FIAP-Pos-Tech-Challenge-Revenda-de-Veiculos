# https://learn.microsoft.com/en-us/azure/aks/learn/quick-kubernetes-deploy-terraform?pivots=development-environment-azure-cli
# terraform init -upgrade
# terraform plan -out main.tfplan
# terraform apply main.tfplan
# terraform plan -destroy -out main.destroy.tfplan
# terraform apply main.destroy.tfplan

resource "azurerm_resource_group" "rg" {
  location = var.resource_group_location
  name     = var.resource_group_name
}

resource "azurerm_kubernetes_cluster" "k8s" {
  location            = azurerm_resource_group.rg.location
  name                = var.kubernetes_cluster_name
  resource_group_name = azurerm_resource_group.rg.name
  dns_prefix          = var.kubernetes_cluster_dns

  identity {
    type = "SystemAssigned"
  }

  default_node_pool {
    name       = "agentpool"
    vm_size    = "Standard_D2ls_v5" #"Standard_D2_v2"
    node_count = var.node_count
  }
  linux_profile {
    admin_username = var.username

    ssh_key {
      key_data = azapi_resource_action.ssh_public_key_gen.output.publicKey
    }
  }
  network_profile {
    network_plugin    = "kubenet"
    load_balancer_sku = "standard"
  }
}