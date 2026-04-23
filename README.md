# Movies API

API RESTful para una plataforma de streaming de películas, construida con arquitectura limpia y las mejores prácticas de .NET.

## Tabla de Contenidos

- [Descripción](#descripción)
- [Tecnologías](#tecnologías)
- [Arquitectura](#arquitectura)
- [Instalación y Ejecución](#instalación-y-ejecución)
- [Características Implementadas](#características-implementadas)
- [Endpoints](#endpoints)

## Descripción

Backend completo para una plataforma de streaming de películas con sistema de perfiles de usuario, gestión de listas personales, autenticación JWT con refresh tokens, y panel de administración.

## Tecnologías

| Capa | Tecnología |
|------|------------|
| Runtime | .NET 10 |
| Framework | ASP.NET Core |
| Base de datos | PostgreSQL |
| ORM | Entity Framework Core |
| Validación | FluentValidation |
| Mapeo | AutoMapper |
| Autenticación | JWT + Refresh Tokens |
| Testing | xUnit + Moq |

## Arquitectura

```
Backend-Movies/
├── Backend-Movies/          # Capa Presentation (API)
│   ├── Controllers/
│   ├── Filters/
│   └── Extensions/
├── Movies.Application/      # Capa Application (Lógica de negocio)
│   ├── Services/
│   ├── DTOs/
│   ├── Validators/
│   └── Interfaces/
├── Movies.Infrastructure/    # Capa Infrastructure (Persistencia)
│   ├── Repositories/
│   ├── Services/
│   ├── Persistence/
│   ├── Middleware/
│   └── External/TMDb/
├── Movies.Domain/            # Capa Domain (Entidades)
│   ├── Entities/
│   └── Enums/
└── Backend-Movies.Test/     # Proyecto de pruebas
```

**Patrón de Arquitectura:** Clean Architecture con separación en capas independientes.

## Instalación y Ejecución

### Requisitos

- .NET 10 SDK
- PostgreSQL

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/clopez9518/dotnet-backend-movies.git
   cd backend-movies
   ```

2. **Configurar variables de entorno**

   Crear archivo `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=movies;Username=postgres;Password=tu_password"
     },
     "Jwt": {
       "Secret": "tu_secret_seguro",
       "Issuer": "MoviesApi",
       "Audience": "MoviesApi",
       "ExpirationMinutes": 60
     },
     "Tmdb": {
       "ApiKey": "tu_api_key_de_tmdb"
     }
   }
   ```

3. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

4. **Ejecutar migraciones**
   ```bash
   dotnet ef database update --project Movies.Infrastructure
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run --project Backend-Movies
   ```

## Características Implementadas

### Autenticación y Autorización
- Registro e inicio de sesión
- JWT Access Tokens (60 min) + Refresh Tokens (7 días)
- Sistema de roles (user/admin)
- Renovación automática de tokens

### Gestión de Perfiles
- Múltiples perfiles por usuario (máx. 5)
- Perfiles con modo infantil (Kids)
- Listas personales de películas

### Catálogo de Películas
- Listado paginado con ordenamiento
- Filtrado por género
- Películas trending
- Película destacada (Hero) con algoritmo de scoring
- Películas similares
- Integración con TMDb API

### Panel de Administración
- CRUD completo de películas
- Gestión de géneros
- Suspensión de usuarios

### Validación
- DTOs validados con FluentValidation
- Middleware centralizado de excepciones

## Endpoints

### Autenticación
| Método | Ruta | Descripción |
|-------|-----|-------------|
| POST | `/api/auth/register` | Registro de usuario |
| POST | `/api/auth/login` | Inicio de sesión |
| POST | `/api/auth/refresh` | Renovar tokens |
| POST | `/api/auth/logout` | Cerrar sesión |
| POST | `/api/auth/select-profile` | Seleccionar perfil activo |

### Películas
| Método | Ruta | Descripción |
|-------|-----|-------------|
| GET | `/api/movies` | Listar películas (paginación) |
| GET | `/api/movies/{id}` | Detalle de película |
| GET | `/api/movies/genre/{genreId}` | Por género |
| GET | `/api/movies/trending` | Tendencias |
| GET | `/api/movies/similar/{id}` | Similares |
| GET | `/api/movies/hero` | Película destacada |

### Perfiles
| Método | Ruta | Descripción |
|-------|-----|-------------|
| GET | `/api/profiles` | Perfiles del usuario |
| POST | `/api/profiles` | Crear perfil |
| GET | `/api/profiles/{id}` | Detalle de perfil |
| DELETE | `/api/profiles/{id}` | Eliminar perfil |
| GET | `/api/profiles/{id}/mylist` | Mi lista |
| POST | `/api/profiles/{id}/mylist` | Agregar a mi lista |
| DELETE | `/api/profiles/{id}/mylist/{movieId}` | Quitar de mi lista |

### Géneros
| Método | Ruta | Descripción |
|-------|-----|-------------|
| GET | `/api/genres` | Listar géneros |
| GET | `/api/genres/{id}` | Detalle de género |

### Administración
| Método | Ruta | Descripción |
|-------|-----|-------------|
| GET | `/api/admin/movies` | Listar películas |
| POST | `/api/admin/movies` | Crear película |
| PUT | `/api/admin/movies/{id}` | Actualizar película |
| PUT | `/api/admin/movies/{id}/genres` | Actualizar géneros |
| GET | `/api/admin/users` | Listar usuarios |
| PUT | `/api/admin/users/{id}/status` | Cambiar estado |
| POST | `/api/admin/genres` | Crear género |
| PUT | `/api/admin/genres/{id}` | Actualizar género |
| GET | `/api/admin/tmdb/search` | Buscar en TMDb |
| GET | `/api/admin/tmdb/{id}` | Detalle desde TMDb |