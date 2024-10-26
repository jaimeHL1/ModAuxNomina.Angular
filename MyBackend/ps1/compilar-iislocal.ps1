#Declaración de variables:
$solutionRoot = $pwd
$projectName = "MyBackend"
$projectRoot = "$solutionRoot\MyBackend"
$nodeVersion = "20.12.0"
#Write-Host "solutionRoot:$solutionRoot"
#Write-Host "projectName:$projectName"
#Write-Host "projectRoot:$projectRoot"



#$procesosProject = "Get-Process MyBackend"
#if($procesosProject.count -gt 0)
# "Get-Process MyBackend | Stop-Process -Force -ErrorAction SilentlyContinue"
$procesos = Get-Process -Name '*w3*' | where-object {$_.CommandLine -like '*ModAuxNomina.Angular*'} #| select-object * -First 1 
if ($procesos) {
    Write-Host "hay procesos del IIS con el nombre 'ModAuxNomina.Angular', detenerlos"
    $procesos | Stop-Process -Force -ErrorAction SilentlyContinue
}

$procesosProject = Get-Process -Name 'ModAuxNomina' -ErrorAction SilentlyContinue
if ($procesosProject) {
    Write-Host "hay procesos con el nombre 'ModAuxNomina', detenerlos"
    $procesosProject | Stop-Process -Force -ErrorAction SilentlyContinue
}

# Verificar si hay procesos con el nombre 'dotnet'
$procesosDotnet = Get-Process -Name 'dotnet' -ErrorAction SilentlyContinue
if ($procesosDotnet) {
     Write-Host "hay procesos con el nombre 'dotnet', detenerlos"
    $procesosDotnet | Stop-Process -Force -ErrorAction SilentlyContinue
}

#Eliminación de carpetas temporales
Write-Host "Ruta del Proyecto:  $($projectRoot)\publish"

if (Test-Path -Path "$($projectRoot)\publish") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\publish"
    Remove-Item -Path "$($projectRoot)\publish" -Recurse -Force
}
if (Test-Path -Path "$($projectRoot)\bin") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\bin"
    Remove-Item -Path "$($projectRoot)\bin" -Recurse -Force
}
if (Test-Path -Path "$($projectRoot)\obj") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\obj"
    Remove-Item -Path "$($projectRoot)\obj" -Recurse -Force
}
# if (Test-Path -Path "$($projectRoot)\MyFrontend-Angular\dist") {
#     Write-Host "Eliminando la carpeta  $($projectRoot)\MyFrontend-Angular\dist"
#     Remove-Item -Path "$($projectRoot)\MyFrontend-Angular\dist" -Recurse -Force
# }

if (Test-Path -Path "$($projectRoot)\MyFrontend-Angular\wwwroot") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\MyFrontend-Angular\wwwroot"
    Remove-Item -Path "$($projectRoot)\MyFrontend-Angular\wwwroot" -Recurse -Force
}


#Compilación y publicación:
cd $projectRoot
dotnet publish --configuration Release --output $projectRoot\publish


###=================================================================================================
## Operaciones angular      
        cd "$($projectRoot)\MyFrontend-Angular"
        Write-Host "-- Instalando Node $($nodeVersion)"
        nvm install $nodeVersion
        nvm use $nodeVersion
        Write-Host "-- Instalando Dependencias  en $($angularfolder)"
        # cambiamos npm install a npm ci para que la instalación de dependencias en node_modules sean exactas al package-lock.json
       #npm ci

        Write-Host "-- Construyendo solución - Configuración $($angularbuild)"
        
        # esta linea es para el pipeline (publicar)
        npm run buildpro 

#Estas líneas que copian los ficheros .js de angular al directorio de salida "\publish\wwwroot", no hacen falta porque cuando
#hacemos "npm run buildpro", la salida está configurada en el fichero angular.json:
# ...
# "architect": {
#        "build": {
#          "builder": "@angular-devkit/build-angular:application",
#          "options": {
#            "outputPath": {
#              "base": "../publish/wwwroot",
#              "browser": ""
#            },
#   ...
#Copy-Item -Path "$($projectRoot)\MyFrontend-Angular\dist\*" -Destination "$($projectRoot)\publish\wwwroot" -Recurse
#Write-Host "-- Copiando ficheros de distribución $($projectRoot)\MyFrontend-Angular\dist\* a $($projectRoot)\publish\wwwroot" 

#Mover carpeta de compilación angular de \MyFrontend-Angular\wwwroot a \publish
Move-Item -Path  "$($projectRoot)\MyFrontend-Angular\wwwroot" -Destination "$($projectRoot)\publish" 
Write-Host "-- Copiando ficheros de distribución $($projectRoot)\MyFrontend-Angular\wwwroot a $($projectRoot)\publish" 

#Ficheros de configuración:
Copy-Item -Path  "$solutionRoot\FicherosConfiguracion\LOCAL-IIS\web.config" -Destination "$($projectRoot)\publish" -Force
Write-Host "-- Copiando fichero web.config $($solutionRoot)\FicherosConfiguracion\LOCAL-IIS\web.config a $($projectRoot)\publish" 

Copy-Item -Path  "$solutionRoot\FicherosConfiguracion\LOCAL-IIS\config.json" -Destination "$($projectRoot)\publish\wwwroot\assets" -Force
Write-Host "-- Copiando fichero config.json $($solutionRoot)\FicherosConfiguracion\LOCAL-IIS\config.json a $($projectRoot)\publish\wwwroot\assets" 

Copy-Item -Path  "$solutionRoot\FicherosConfiguracion\LOCAL-IIS\appsettings.json" -Destination "$($projectRoot)\publish" -Force
Write-Host "-- Copiando fichero config.json $($solutionRoot)\FicherosConfiguracion\LOCAL-IIS\appsettings.json a $($projectRoot)\publish" 