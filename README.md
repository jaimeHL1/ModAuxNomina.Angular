# Nuevo proyecto ConsultaNomina
El proyecto tiene una aplicación Backend en NET8 y una aplicación FrontEnd-Angular 

Vamos a seguir los pasos del proyecto Angular-NET8 del área de formación para la creación del proyecto
## 1. Desde la rama develop creamos una nueva rama que se va a llamar angular-net8
## 2. Borramos todos los ficheros excepto README.md
## 3. Creamos el proyecto net8:
```powershell
    #Desde powershell ejecutamos este comando para crear una solución vacía:
    dotnet new sln -n ConsultaNomina
    #Este comando crea un proyecto de tipo webapi llamado MyBackend y machaca todo lo que pueda haber antes
    # https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-sdk-templates#webapi
    dotnet new webapi -n MyBackend --force
    #Este comando añade a la solución creada en el primer paso el proyecto del segundo
    dotnet sln add MyBackend/MyBackend.csproj
    # compilar y ejecutar. se levanta el localhost en un puerto y responde en http://localhost:puerto/weatherforecast
    cd MyBackend
    dotnet run
```
    Copiar program.cs de ConsultaNomina y adaptarlo (comentar modelos)
    Agregar en csproj los ItemGroup de ConsultaNomina
    dotnet run

### 3.1 Copiar siguientes ficheros y carpetas de ConsultaNomina
    .vscode
    ps1
    FicherosConfiguracion
    .gitignore


### 3.2 Depuración del NET8 en visual code
    - Depuración automática:
        Creamos Launch.json --> Hacemos clic en la cucaracha, arriba saldrá una flecha verde, al lado pinchamos en una rueda 
                                + add configuration 
                                + Seleccionar .NET C# PROJECT
                                + Ajustar la ruta del proyecto. Ejemplo  "projectPath": "${workspaceFolder}\\MyBackend\\MyBackend.csproj"
    - Depuración personalizada:
        Creamos otra configuración en launch.json "1- Depurar .NET Core",
            La línea  "program": "${workspaceFolder}/MyBackend/bin/Debug/net8.0/ModAuxNomina.dll" es la que lanza el servidor kestrel

        Creamos tasjs.json llamada por "1- Depurar .NET Core" que llama a compilarNET8.ps1, también creado.
        Lanzamos la depuración personalizada.
        
     - Importante: Para la correcta compilación del NET8, debemos tener  sdk-8.0.300
        url:https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.300-windows-x64-installer

## 4. Copiar MyFrontend-Angular de ConsultaNomina entero
   
 ### 4.2 Depuración de Angular
        Necesitamos dos configuraciones
            1. Hacer npm run start = ng serve. Esta configuración llamada "Ejecutar Node.js", en el launch.json levanta el servidor Node enlazado con el código fuente de angular.
            2. Levantamos un chrome con los símbolos del ángular para poder depurar (configuración "Launch Chrome for Node")
            Importante: Para que funcionen los servicios tenemos que levantar el kestrel y por tanto el punto 2, necesita
            una tarea previa, que es levantar el kestrel.
        En el launch.json, declaramos un compounds "2-Debug Angular 4200 Node.js y Chrome", que ejecuta las dos configuraciones

 ## 5. Publicación en IISLocal
        1. Modificación angular.json en 
              "build": {
                    ...
                "configurations": {
                    ...
                    "production": {
                        ...
                         "baseHref": "/ConsultaNomina/"
                    }
                }
            }
        1. publicar.ps1 Este fichero es lanzado en el launch.json por la task "Publicar solución Para IIS Local"            
            El fichero publicar.ps1 realiza las siguientes tareas:
                - Mata el proceso del pool del IIS en el caso de que esté activo
                - Mata el proceso kestrel si está levantado
                - Mata el proceso dotnet si existe 
                - Elimina las carpetas de prepublicación (bin y obj) publicación (publish), de los fuentes angular y net8
                - Instala node 20.12.0, instala paquetes y dependencias de angular (node_modules) y lanza la publicación en modo buildprod
                - Complia el proyecto .Net para su distribución en modo Release en el directorio /publish
                - Copia los ficheros compilados para la publicación a /publish y los transpilados a /publish/wwwroot

## 6. Esqueleto carpetas aplicación .net core8 + angular18^
    F:\Devops\AreaDesarrollo\ConsultaNomina.Angular\
        |-- MyBackend
            |-- bin/
            ├── BL/
                |-- ConsultaDbNedaes.cs
            ├── controllers/           
                |-- ConsultaDBNedaesController.cs
            ├── DA/
                |-- DB_Nedaes.cs
            ├── Json/
                |-- NominasMes.json
                |-- Meses.json
            ├── models
                |-- AppSettingsModel.cs
                |-- Concepto.cs
                |-- Mes.cs
                |-- Nomina.cs
                |-- NominaMes.cs
                |-- Recibo.cs
            ├── obj/
            |-- appsettings.json
            |-- program.cs	
            |-- MyBackend.csproj
            |-- ConsultaNomina.sln
            ├── publish/ (generado publicación)
                |-- ficheros .dll (publicación)
                ├── wwwroot/
                    |-- ficheros .js (estáticos angular)
            ├── MyFrontend-Angular/   
                |-- node_modules
                |-- public
                |-- package.json
                |-- angular.json     
                |-- tsconfig.json
                |-- tsconfig.app.json               
                ├── src/
                │   ├── app/
                │   │   ├── @pages/
                │   │   │   |-- inicio.component.ts
                │   │   │   |-- inicio.component.html
                │   │   │   |-- inicio.component.css
                │   │   |-- app.component.ts
                │   │   |-- app.component.html
                │   │   |-- app.component.css
                │   │   |-- app-routing.module.ts
                │   │   |-- app.module.ts
                │   ├── assets/
                │   ├── environments/
                │   |-- index.html
                │   |-- main.ts
                │   |-- styles.css
 ### 6.1 El esqueleto básico de angular incluye:
 
   - Un componente InicioComponent en la carpeta @pages.
   - El componente principal AppComponent que actúa como contenedor.
   - Configuración de rutas en app-routing.module.ts, con una ruta por defecto que apunta a InicioComponent y una redirección para rutas no encontradas.
   - La configuración del módulo principal AppModule que importa y declara los componentes necesarios.
  
# pbi-integracion-continua
Ocurre cuando se termina una tarea
## objetivo
Cuando terminamos una tarea y hacemos la pull request de su rama en develop, se producirá de forma automática un despliegue
en el servidor de desarrollo.
Cuando ejecutamos la pipeline en una rama "release" o se hace una pull request en "main", se despliega automáticamente en //drops
## tareas previas
 - El desarrollador comprueba que su tarea está terminada en su IISLocal
 - Guardamos cambios (commit) 
 - Integramos cambios de develop en nuestra rama con rebase
 - Comprobamos en el IISLocal que todo sigue funcionando
 - Guardamos cambios en remoto (push)
 - Hacemos pull request a develop
## ejecución automática de la pipeline

El objetivo a conseguir es crear una pipeline que se ejecutará automáticamente con cualquier pull request a develop. 
## Comprobar ficheros de configuración del angular package.json y angular.json, la salida en el angular.json es a la carpeta ".\wwwroot"
## Crear pipeline con las siguientes tareas:
- Compilar angular 
- Compilar Net
- Copiar wwwroot a publish
- Publicar compilación a artefacto
## Crear release para pulicar en desarrollo
## Crear release para publicar en drops