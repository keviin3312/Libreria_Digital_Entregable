# Librería Digital - ASP.NET Core Web API

Proyecto base para el reto de Librería Digital.

## Funcionalidades
- Crear, listar, editar y eliminar usuarios.
- Crear, listar, editar y eliminar libros por usuario.
- Calificar libros de 1 a 5 y crear reseñas.
- Swagger habilitado.
- SQL Server con Docker.

## Requisitos
- Docker Desktop
- O Visual Studio 2022 con .NET 8 SDK

## Ejecutar con Docker
```bash
docker compose up --build
```

Swagger:
```text
http://localhost:8080/swagger
```

## Ejecutar en Visual Studio
1. Abrir `KtorresLibreriaDigital.csproj`.
2. Restaurar paquetes NuGet.
3. Verificar que SQL Server esté disponible en `localhost,1433`.
4. Ejecutar el proyecto.
5. Abrir Swagger en `http://localhost:5080/swagger`.

## Notas
- El proyecto usa `EnsureCreated()` para crear la base automáticamente al iniciar.
- Para una versión más profesional, después puedes cambiar a migraciones con `dotnet ef`.

## Libreria_Digital_Entregable
Proyecto WebApi en C# de libreria digital

## Ejecucion del docker:
se debe ejecutar el siguiente comando en la ruta del proyecto para levantar el contenedor de SQL Server y la API:
docker compose up -d --build