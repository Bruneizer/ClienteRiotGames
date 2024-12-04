# ClienteRiotGames
<h1 align="center"> Mordekaiser </h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/et12.gif">
</p>

<h2 align="center"> E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín" </h2>

**Alumnos**: Luis Armoa, Carlos Bruno, Ruben Torrent
**Asignatura**:  AGBD
**Nombre TP**: Mordekaiser
**Curso**: 5 ° 7
**Año**:  2024

# Mordekaiser

Nuestro proyecto es una base de datos diseñada para almacenar y gestionar información del cliente de Riot Games relacionada con el juego Valorant y el juego League of Legends. Esta base de datos tiene como objetivo proporcionar una estructura organizada para el almacenamiento de datos de cuentas, objetos, inventarios y servidores. El proyecto busca implementar una solución escalable y eficiente para el manejo de grandes cantidades de datos, permitiendo a los usuarios interactuar con la base de datos de manera sencilla y segura.

## Comenzando 

Clonar el repositorio github, desde Github Desktop o ejecutar en la terminal o CMD:

```
https://github.com/Armoaaa/MORDEKAISER
```

## Pre-requisitos 

- .NET 8 (SDK .NET 8.0.105) - [Descargar](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
- Visual Studio Code - [Descargar](https://code.visualstudio.com/#alt-downloads)
- Git - [Descargar](https://git-scm.com/downloads)
- MySQL - [Descargar](https://dev.mysql.com/downloads/mysql/)
- Dapper - Micro ORM para .NET
- Entity Framework Core - Para la gestión de datos en .NET

## Pasos para iniciar el proyecto 

_Para iniciar el proyecto primero debe desplegar la base de datos y para eso tiene que hacer segundo click en la carpeta scripts bd_
_y presionar en terminal integrado, le aparecera una terminal donde tiene que poner lo siguiente:_

```
mysql -u tuUsuarioMySql -p 
:tuContraseñaMySql
```

_Luego dirigirse a la carpeta src y dentro de la carpeta Mordekaiser.ReposDapper.Test_

1. Crear `appSettings.json`: nombre del archivo json que tiene que estar en la misma carpeta.
El contenido del archivo tiene que ser:  
  ```json
  {
  "ConnectionStrings": {
    "MySQL": "server=localhost;database=tuBD;user=tuUsuarioBD;password=tuPassBD"
  }
  }
  ```

Para desplegar el proyecto, sigue los siguientes pasos:


1. **Abrir el proyecto**:
   - Navega al directorio del proyecto clonado:
     ```
     cd MORDEKAISER
     ```
   - Abre el proyecto en Visual Studio Code ejecutando:
     ```
     code .
     ```


2. **Configurar la base de datos**:
   - Asegúrate de tener MySQL instalado y en funcionamiento.
   - Crea una base de datos llamada `ClienteRiotGames` en tu servidor MySQL.
   - Navega a la carpeta `scripts bd` dentro del proyecto:
     ```
     cd scripts bd
     ```
   - Ejecuta el siguiente comando para importar los scripts SQL necesarios:
     ```
     mysql -u UsuarioMySQL -p
     ```
   - Luego, ingresa la contraseña de tu usuario y ejecuta:
     ```
     SOURCE install.sql
     ```

3. **Ejecutar el proyecto**:
   - Regresa al directorio raíz del proyecto:
     ```
     cd ..
     ```
   - Ejecuta el proyecto utilizando el siguiente comando:
     ```
     dotnet run
     ```

4. **Probar el proyecto**:
   - Para ejecutar las pruebas unitarias, dirigirse a la carpeta ClienteRiotGames.Dapper.Test (segundo clic en la carpeta y abrir en terminal integrado) y utilizar el siguiente comando:
     ```
     dotnet test
     ```

5. **Acceder a la aplicación**:
   - Una vez que el proyecto esté en ejecución, podrás acceder a la aplicación a través de tu navegador en la dirección que se indique en la terminal (generalmente `http://localhost:5000` o similar).


## Herramientas

El proyecto fue construido utilizando las siguientes herramientas y versiones:

* .NET 8 (SDK .NET 8.0.105)
* Visual Studio Code
* SQL Server
* Dapper (versión 2.1.35)
* MySQL (versión 8.0 o superior)
* XUnit (para pruebas unitarias)
<!-- agarre de base el redme del equipo spotify XD peldon por el pecado no tenia tiempo -->
 
