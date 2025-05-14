variable "resource_group_location" {
  type        = string
  default     = "Brazil South"
  description = "Location of the resource group."
}

variable "resource_group_name" {
  type        = string
  default     = "fiap-pos-tech-aks-rg"
  description = "Resource group name in your Azure subscription."
}

variable "kubernetes_cluster_name" {
  type        = string
  default     = "fiap-pos-tech-k8s-cluster"
  description = "Kubernetes cluster name in your Azure subscription."
}

variable "kubernetes_cluster_dns" {
  type        = string
  default     = "fiap-pos-tech-k8s-dns"
  description = "Kubernetes cluster dns in your Azure subscription."
}

variable "node_count" {
  type        = number
  description = "The initial quantity of nodes for the node pool."
  default     = 3
}

variable "msi_id" {
  type        = string
  description = "The Managed Service Identity ID. Set this value if you're running this example using Managed Identity as the authentication method."
  default     = null
}

variable "username" {
  type        = string
  description = "The admin username for the new cluster."
  default     = "azureadmin"
}

variable "ssh_key_name" {
  type        = string
  description = "The ssh key for the new cluster."
  default     = "azapisshkey"
}