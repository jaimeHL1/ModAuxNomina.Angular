# declaración de variables
$solutionRoot = $pwd
$projectName = "MyBackend"
$projectRoot = "$solutionRoot\MyBackend"
Write-Host "solutionRoot:$solutionRoot"
Write-Host "projectName:$projectName"
Write-Host "projectRoot:$projectRoot"

 
cd $projectRoot
 
if (Test-Path -Path "$($projectRoot)\bin") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\bin"
    Remove-Item -Path "$($projectRoot)\bin" -Recurse -Force
}
if (Test-Path -Path "$($projectRoot)\obj") {
    Write-Host "Eliminando la carpeta  $($projectRoot)\obj"
    Remove-Item -Path "$($projectRoot)\obj" -Recurse -Force
}
 
dotnet build