# Convenciones — WebMultiempresa

Fuente de verdad para el equipo de desarrollo. Ante cualquier duda, esta guía prevalece.

## C# — Reglas estrictas

| Regla | Correcto | Prohibido |
|---|---|---|
| Tipos explícitos | `string nombre = ...` | `var nombre = ...` |
| Async | `await repo.GetAsync(ct)` | `.Result`, `.GetAwaiter().GetResult()` |
| HTTP | `IHttpClientFactory` | `new HttpClient()` |
| DI | Constructor injection | `ServiceLocator`, `GetService<T>()` en domain/app |
| Reads EF | `.AsNoTracking()` | Tracking en queries de solo lectura |
| Transferencia de datos | DTOs/records | Exponer `Entity` en Blazor |

## Comentarios e identificadores

- **Comentarios:** español. Ej: `// Valida que el combo no esté dado de baja`
- **Identificadores:** inglés. Ej: `GetActiveProductsQuery`, `comboRepository`
- Sin comentarios triviales — solo cuando el "por qué" no es obvio

## Nullable Reference Types

- Proyecto compilado con `<Nullable>enable</Nullable>`
- No usar `!` para suprimir nullability warnings sin un comentario que lo justifique
- Inicializar propiedades de entidad con `= string.Empty` o valor apropiado

## Entidades de dominio

- Constructor privado + factory method estático: `Empresa.Crear(...)`
- Setters privados — estado mutable solo via métodos del dominio
- Sin dependencias externas en entidades

## Stored Procedures

Naming: `<Tabla>_<Operacion><DescripcionPascalCase>`

```
Combos_TXObtenerPorId
Combos_ACrear
Combos_MActualizarVigencia
Combos_EEliminar
```

Archivo por SP en: `sql-sp/<Tabla>/<NombreSP>.sql`
Script completo con `CREATE OR ALTER PROCEDURE`.

## Organización de archivos

- Un archivo por clase/interface
- Namespace = estructura de carpetas: `WebMultiempresa.Application.Commands.Combos`
- Agrupación por feature, no por tipo: `Commands/Combos/`, no `Commands/AllCommands/`

## Patrones prohibidos

```csharp
// PROHIBIDO
var x = repo.GetAsync().Result;
new HttpClient();
Thread.Sleep(1000);
Console.WriteLine("debug");
```

## CancellationToken

Pasar `CancellationToken` en:
- Todos los métodos de repositorio
- Todos los `CommandHandler` y `QueryHandler`
- Todos los métodos de servicio llamados desde Blazor
