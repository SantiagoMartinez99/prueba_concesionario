
# Proyecto de Gestión de Vehículos

Este proyecto es una aplicación para la gestión de vehículos de un concesionario, que incluye tanto un backend desarrollado en .NET como un frontend en React. A continuación, se describen los pasos necesarios para ejecutar ambas partes del proyecto.

## Requisitos Previos

Asegúrate de tener instalados los siguientes programas en tu máquina:

- [.NET SDK](https://dotnet.microsoft.com/download) (versión 8.0 o superior)
- [Node.js](https://nodejs.org/) (versión 14.0 o superior)
- [npm](https://www.npmjs.com/get-npm) (se incluye con Node.js)
- [Visual Studio](https://visualstudio.microsoft.com/) o cualquier IDE de tu preferencia para .NET (opcional)

## Configuración del Backend (.NET)

1. **Clonar el Repositorio**

   Abre una terminal y clona el repositorio:

   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd <NOMBRE_DEL_REPOSITORIO>

2. **Restaurar Dependencias**

Navega a la carpeta del backend y restaura las dependencias:

	
	cd backend
	dotnet restore

3.  **Ejecutar la Aplicación**

Inicia el backend:

	  dotnet run

## Configuración del Frontend (React)
1. **Navegar a la Carpeta del Frontend**
Regresa a la raíz del proyecto y navega a la carpeta del frontend:

	```bash
	cd ../frontend
2. **Instalar Dependencias**
Instala las dependencias del frontend usando npm:

		npm install

3. **Ejecutar la Aplicación**
	```bash
	npm start
La aplicación debería abrirse en http://localhost:5173.

4. **Estructura del Proyecto**
backend/: Contiene el código del backend desarrollado en .NET.
frontend/: Contiene el código del frontend desarrollado en React.


