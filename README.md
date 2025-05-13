# Ejecución Dinámica de Flujos

Este proyecto consiste en una API que ejecuta flujos dinámicos definidos por pasos, los cuales pueden tener dependencias configurables. La ejecución de estos pasos se realiza de forma dinámica según su configuración, con la posibilidad de ejecutar pasos en paralelo si no tienen dependencias.

---

##  Funcionalidades implementadas

- ✅ Ejecución dinámica de pasos con base en su configuración.
- ✅ Soporte para **ejecución en paralelo** de pasos independientes.
- ✅ Uso del patrón **Chain of Responsibility** para manejar la lógica de ejecución uno a uno.
- ✅ Soporte para que un paso dependa de **un solo paso anterior** (por limitación de tiempo).
- ✅ Validación de campos requeridos antes de ejecutar un paso.
- ✅ Marcado de pasos como completados una vez procesados correctamente.

> ⚠️ Nota: En esta versión, cada paso solo puede depender de **un paso anterior**, pero la arquitectura permite extenderlo para soportar múltiples dependencias en el futuro.

---

##  Requisitos

- SQL Server
- Entity Framework Core

---

## Pasos para levantar el proyecto

### 1. Clonar el repositorio

Clona este repositorio en tu máquina local:

```bash
git clone https://github.com/samcep/flows-app.git
```

### 2. Abrir el proyecto en Visual Studio

1. Abre Visual Studio.
2. Ve a **Archivo** > **Abrir** > **Proyecto/Solución y/o .csproj**.
3. Selecciona el archivo `flows-app.csproj` y/o `flows-app.sln` que se encuentra en la raíz del proyecto clonado .Asegúrate de abrirlo en visual studio.

### 3. Configurar la cadena de conexión

La cadena de conexión se encuentra en el archivo `appsettings.Development.json`. Asegúrate de configurar tu base de datos SQL Server.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Database={{YOUR_DATABASE}}; Integrated Security=True; TrustServerCertificate=True"
  }
}
```

Asegúrate de que los valores en la cadena de conexión sean correctos:

- **Server**: Dirección del servidor de la base de datos (puede ser `localhost` o el nombre del servidor si estás usando SQL Server en otro equipo).
- **Database**: El nombre de la base de datos.
- **Trusted_Connection**: Si usas autenticación de Windows, asegúrate de que esté configurado en `True`. Si usas autenticación de SQL Server, necesitarás especificar el usuario y la contraseña en la cadena.

### 4. Restaurar los paquetes NuGet

Una vez que hayas abierto el proyecto en Visual Studio, restaura los paquetes NuGet necesarios para que el proyecto funcione:

1. Abre el **Administrador de Paquetes NuGet** desde **Herramientas** > **Administrador de Paquetes NuGet** > **Consola del Administrador de Paquetes**.
2. Ejecuta el siguiente comando en la consola para restaurar los paquetes NuGet:

```bash
dotnet restore
```
## Opción 2: Usar el Menú de Visual Studio
Haz clic derecho sobre la solución en el Explorador de Soluciones.
Selecciona la opción Restaurar paquetes NuGet.
Esto descargará e instalará todos los paquetes necesarios para el proyecto

### 5. Crear y aplicar migraciones

A continuación, necesitarás generar las migraciones para crear la estructura de la base de datos.

1. **Crear una migración**:

   Ejecuta el siguiente comando en la Consola del Administrador de Paquetes para crear una nueva migración:

   ```bash
   Add-Migration InitialCreate
   ```

   Esto generará los archivos necesarios para crear la base de datos y las tablas definidas en el proyecto.

2. **Actualizar la base de datos**:

   Después de crear la migración, ejecuta el siguiente comando para aplicar las migraciones y crear la base de datos:

   ```bash
   Update-Database
   ```

   Esto aplicará la migración y creará la base de datos con las tablas necesarias  y su respectiva informacionn.

### 6. Ejecutar el proyecto

Para ejecutar el proyecto:

1. Asegúrate de que el proyecto está configurado correctamente en **Visual Studio**.
2. Presiona **Ctrl+F5** para ejecutar el proyecto sin depuración o **F5** para ejecutarlo con depuración.

---

