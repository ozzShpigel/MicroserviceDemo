variable "location" {}
variable resource_group_name {}
variable name {}
variable docker_image{}
variable docker_image_tag{}
variable "connection_value" {}
variable "docker_registry_password" {
  type        = string
  description = "Docker Registry password"
}
variable "docker_registry_url" {
  type        = string
  description = "Docker Registry URL"
}
variable "docker_registry_username" {
  type        = string
  description = "Docker Registry username"
}