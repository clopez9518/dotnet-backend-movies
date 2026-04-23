# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Copiamos los archivos .csproj para aprovechar el cache de Docker en el restore
COPY Movies.Domain/Movies.Domain.csproj Movies.Domain/
COPY Movies.Application/Movies.Application.csproj Movies.Application/
COPY Movies.Infrastructure/Movies.Infrastructure.csproj Movies.Infrastructure/
COPY Backend-Movies/Movies.Api.csproj Backend-Movies/

# 2. Restauramos las dependencias
# Nota: Al restaurar el API, .NET resuelve automáticamente las dependencias de los otros proyectos
RUN dotnet restore Backend-Movies/Movies.Api.csproj

# 3. Copiamos todo el código fuente
COPY Movies.Domain/ Movies.Domain/
COPY Movies.Application/ Movies.Application/
COPY Movies.Infrastructure/ Movies.Infrastructure/
COPY Backend-Movies/ Backend-Movies/

# 4. Publicamos la aplicación
RUN dotnet publish Backend-Movies/Movies.Api.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# 5. Seguridad: .NET 8/10 ya incluye el usuario 'app'. 
# Solo nos aseguramos de que los archivos le pertenezcan.
COPY --from=build /app/publish .
RUN chown -R app:app /app
USER app

# 6. Configuración de Red para Render
# Usamos el puerto dinámico que Render inyecta ($PORT)
ENV ASPNETCORE_URLS=http://+:${PORT:-10000}
ENV ASPNETCORE_ENVIRONMENT=Production

# Exponemos el puerto (informativo para Docker, pero Render usa la variable ENV)
EXPOSE 10000

# 7. Punto de entrada
ENTRYPOINT ["dotnet", "Movies.Api.dll"]