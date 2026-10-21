# Configuración para Azure App Service

La API obtiene la conexión a Azure SQL desde `ConnectionStrings:DefaultConnection`. Los datos no se guardan en el código fuente.

En **App Service > Configuration > Application settings**, agrega:

- `ConnectionStrings__DefaultConnection`: cadena de conexión de Azure SQL. Usa el servidor `tcp:<servidor>.database.windows.net,1433`, la base `Mini_FerreteriaDT`, autenticación SQL o Microsoft Entra, `Encrypt=True` y `TrustServerCertificate=False`.
- `Jwt__Key`: secreto de al menos 32 caracteres.
- `Jwt__Issuer`: emisor configurado para los tokens de la aplicación.
- `Jwt__Audience`: audiencia configurada para los tokens.

Guarda y reinicia el App Service después de agregar los valores. No guardes contraseñas ni claves JWT en `appsettings.json` ni en Git.

## Base de datos

`AppDbContext` ahora declara las entidades principales del diagrama y sus claves y relaciones. Esto **no crea por sí solo las tablas**: el proyecto no incluye una migración inicial ni llama `Database.Migrate()` durante el arranque. Crea primero el esquema en la base Azure con el script SQL corregido y luego despliega la API.

El proyecto contiene llamadas ADO.NET además de EF Core. Los accesos a Producto, Categoría, Proveedor e Inventario se cambiaron para usar la misma cadena configurada para Azure SQL en lugar de una dirección de servidor local.
