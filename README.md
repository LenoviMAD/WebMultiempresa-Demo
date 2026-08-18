# WebMultiempresa

Portal multiempresa para gestionar vendedores, clientes, productos y combos comerciales. Lo armé con **Blazor Server + Clean Architecture (DDD/Hexagonal) + EF Core** sobre **.NET 9** para tener un proyecto completo de punta a punta que muestre autenticación, autorización, multiempresa y una arquitectura en capas bien separada.

Corre 100% en local, sin depender de ningún servidor externo: al ejecutar la aplicación, la base de datos se crea y se siembra sola con datos de ejemplo.

## Stack

- .NET 9 / C#
- Blazor Server
- EF Core + SQL Server LocalDB
- JWT (bearer token) para autenticación
- BCrypt para hash de contraseñas

## Arquitectura

DDD + Hexagonal (Ports & Adapters)

```
src/
├── WebMultiempresa.Domain/          # Entidades y reglas de negocio
├── WebMultiempresa.Application/     # Casos de uso (commands/queries)
├── WebMultiempresa.Infrastructure/  # EF Core, repositorios, seeder
└── WebMultiempresa.Blazor/          # UI Blazor Server
tests/
├── WebMultiempresa.UnitTests/
└── WebMultiempresa.IntegrationTests/
```

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- **SQL Server LocalDB** (`MSSQLLocalDB`) — viene incluido con Visual Studio (carga de trabajo ".NET desktop development" o "ASP.NET and web development"), o se puede instalar de forma standalone con **SQL Server Express LocalDB** ([descarga](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)).

No hace falta ningún servidor SQL externo, VPN, ni credenciales de terceros. Todo corre contra la instancia local `(localdb)\MSSQLLocalDB`.

## Cómo correrlo

```bash
# Restaurar dependencias
dotnet restore

# Correr la aplicación (crea y siembra la base la primera vez)
dotnet run --project src/WebMultiempresa.Blazor
```

Al arrancar por primera vez, la aplicación:

1. Aplica las migraciones de EF Core contra `(localdb)\MSSQLLocalDB`, creando la base `WebMultiempresaDemo`.
2. Siembra datos de ejemplo (ver `src/WebMultiempresa.Infrastructure/Persistence/DbSeeder.cs`): una empresa, usuarios, vendedores, clientes y productos, todos ficticios.

No hace falta ningún paso manual adicional ni scripts de seed por fuera del propio `dotnet run`.

## API a la que se conecta

Ninguna — es standalone. La UI (Blazor Server) habla directo con su propia base de datos vía EF Core.

## Credenciales

| Usuario | Password | Rol |
|---|---|---|
| `super@admin.com` | `Super123!` | SuperAdmin (acceso a todas las empresas) |
| `admin@demo.com` | `Admin123!` | Admin de la Empresa Demo |
| `operador@demo.com` | `Operador123!` | Operador de la Empresa Demo |

## Tests

```bash
dotnet test
```

## Nota sobre los datos

Todos los datos (empresa, usuarios, vendedores, clientes, productos) son ficticios, pensados para poder mostrar el proyecto funcionando sin depender de información real de ningún tipo.
