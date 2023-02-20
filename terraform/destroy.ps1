
param (
    $ARM_SUBSCRIPTION_ID=$env:ARM_SUBSCRIPTION_ID,
    $ARM_TENANT_ID=$env:ARM_TENANT_ID,
    $ARM_CLIENT_ID=$env:ARM_CLIENT_ID,
    $ARM_CLIENT_SECRET=$env:ARM_CLIENT_SECRET,
    $ARM_ACCESS_KEY=$env:ARM_ACCESS_KEY,
    $CONTAINER_REGISTRY_PASSWORD=$env:CONTAINER_REGISTRY_PASSWORD,
    $CONNECTIONSTRING_CUSTOMER= $env.SQLSERVER_CONNECTIONSTRING_CUSTOMER,
    $CONNECTIONSTRING_ORDER=$env.SQLSERVER_CONNECTIONSTRING_ORDER,
    $SERVICEBUS_TOKEN=$env.SERVICEBUS_TOKEN
    )
    
    $rootPath = $PWD
    $folders = gci -Exclude "storage-account","modules" -Directory
    
    
    foreach ($folder in $folders.name)
    {
      cd $folder  
      Write-Host "terrafrom destroy: $folder"

      terraform init -var access_key=$ARM_ACCESS_KEY -var tenant_id=$ARM_TENANT_ID -var client_id=$ARM_CLIENT_ID -var client_secret=$ARM_CLIENT_SECRET -var subscription_id=$ARM_SUBSCRIPTION_ID

      if($folder -eq "app"){
        az keyvault secret delete --name AzBusSecret --vault-name ozzkeyvault
        terraform destroy --auto-approve -var docker_registry_password=$CONTAINER_REGISTRY_PASSWORD -var customer_connection=$CONNECTIONSTRING_CUSTOMER -var order_connection=$CONNECTIONSTRING_ORDER -var access_key=$ARM_ACCESS_KEY -var tenant_id=$ARM_TENANT_ID -var client_id=$ARM_CLIENT_ID -var client_secret=$ARM_CLIENT_SECRET -var subscription_id=$ARM_SUBSCRIPTION_ID -lock=false
        az keyvault secret purge --name AzBusSecret --vault-name ozzkeyvault
      }
      elseif($folder -eq "functionapp"){
        az keyvault secret delete --name AzBusSecret --vault-name ozzkeyvault
        terraform destroy --auto-approve -var docker_registry_password=$CONTAINER_REGISTRY_PASSWORD -var servicebus_key=$SERVICEBUS_TOKEN -var order_connection=$CONNECTIONSTRING_ORDER -var access_key=$ARM_ACCESS_KEY -var tenant_id=$ARM_TENANT_ID -var client_id=$ARM_CLIENT_ID -var client_secret=$ARM_CLIENT_SECRET -var subscription_id=$ARM_SUBSCRIPTION_ID -lock=false
      }
      else{
        terraform destroy --auto-approve -var access_key=$ARM_ACCESS_KEY -var tenant_id=$ARM_TENANT_ID -var client_id=$ARM_CLIENT_ID -var client_secret=$ARM_CLIENT_SECRET -var subscription_id=$ARM_SUBSCRIPTION_ID -lock=false
      }
      cd $rootPath
    }