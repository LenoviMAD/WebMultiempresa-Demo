# Arquitectura — WebMultiempresa

## Visión general

Sistema multiempresa de gestión comercial construido en Blazor Server con
arquitectura DDD + Hexagonal (Ports & Adapters).

## Capas

```
┌─────────────────────────────────────┐
│  WebMultiempresa.Blazor             │  UI — Componentes Razor, auth, routing
├─────────────────────────────────────┤
│  WebMultiempresa.Application        │  Casos de uso, Commands, Queries, DTOs, Ports
├─────────────────────────────────────┤
│  WebMultiempresa.Infrastructure     │  EF Core, Repositorios, JWT, BCrypt
├─────────────────────────────────────┤
│  WebMultiempresa.Domain             │  Entidades, Value Objects, Interfaces
└─────────────────────────────────────┘
```

**Regla de dependencias:** cada capa solo depende de la capa inmediata inferior.
`Domain` no depende de nada externo.

## Flujo de una operación típica (Command)

```
Blazor Page → ICommandHandler<TCommand> → CommandHandler → IRepository → EF Core → SQL Server
```

## Flujo de una consulta (Query)

```
Blazor Page → IQueryHandler<TQuery, TResult> → QueryHandler → IRepository.AsNoTracking() → DTO
```

## Autenticación

- Login: `LoginCommand` → `LoginCommandHandler` → JWT firmado
- Blazor: `BlazorAuthStateProvider` consume el token desde sesión
- Rutas protegidas: `[Authorize]` o `<AuthorizeView>`

## Base de datos

- ORM: EF Core 9 con SQL Server
- Migraciones: `Infrastructure/Migrations/`
- Configuraciones de entidad: `Infrastructure/Persistence/Configurations/`
- Stored Procedures: `sql-sp/<Tabla>/` — invocados via `FromSqlRaw` o `ExecuteSqlRaw`

## Dominio multiempresa

Cada empresa tiene `KeyConexion` que identifica su contexto de datos.
Los combos, listas de precios, rubros y marcas son configurables por empresa.

## Tests

| Proyecto | Tipo | Dependencias |
|---|---|---|
| `WebMultiempresa.UnitTests` | Unitario | Sin externos |
| `WebMultiempresa.IntegrationTests` | Integración | Testcontainers (Docker) |
