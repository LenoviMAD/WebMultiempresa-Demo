# WebMultiempresa — Resumen Técnico para PM

> **Versión:** 1.0 — Mayo 2026
> **Audiencia:** Project Manager / Product Owner del equipo de desarrollo

---

## 1. Nombre y propósito del proyecto

**WebMultiempresa** es un portal web de administración centralizada que permite gestionar múltiples empresas desde una única plataforma. Cada empresa opera con su propio catálogo de productos, vendedores, listas de precios, combos comerciales y usuarios, sin que los datos de una empresa sean visibles para otra.

El sistema está diseñado para ser el panel de control que alimenta aplicaciones satélite (app de ventas, app de fleteros, app de compras, GPS) que consumen los datos aquí registrados.

---

## 2. Stack tecnológico

| Componente | Tecnología |
|---|---|
| Lenguaje | C# 13 / .NET 9 |
| UI | Blazor Server (renderizado en servidor, sin SPA separado) |
| Base de datos | SQL Server |
| ORM | Entity Framework Core + SqlServer Provider |
| Autenticación | JWT (JSON Web Tokens) |
| Hashing de contraseñas | BCrypt |
| Tests unitarios | xUnit |
| Tests de integración | xUnit + Testcontainers (SQL Server real en Docker) |
| Contenedor | No — despliegue directo en servidor Windows/IIS o Kestrel |

---

## 3. Arquitectura del sistema

El proyecto sigue el patrón **DDD + Arquitectura Hexagonal (Ports & Adapters)**. Esto separa estrictamente la lógica de negocio de los detalles técnicos (base de datos, UI).

```
┌─────────────────────────────────────────────┐
│         WebMultiempresa.Blazor              │  ← Interfaz web (Razor/Blazor)
│         Pages + Shared + Auth               │
└────────────────────┬────────────────────────┘
                     │ usa
┌────────────────────▼────────────────────────┐
│       WebMultiempresa.Application           │  ← Casos de uso, Commands, Queries, DTOs
│       Commands / Queries / Ports / DTOs     │
└────────────────────┬────────────────────────┘
                     │ implementa
┌────────────────────▼────────────────────────┐
│      WebMultiempresa.Infrastructure         │  ← Repositorios EF Core, servicios JWT,
│      Repositories / Services / EF Config    │    contextos de sesión
└────────────────────┬────────────────────────┘
                     │ define entidades
┌────────────────────▼────────────────────────┐
│         WebMultiempresa.Domain              │  ← Entidades de negocio, enums, interfaces
│         Entities / Enums / Interfaces       │
└─────────────────────────────────────────────┘
```

**Regla clave:** La capa de dominio no conoce nada de la UI ni de la base de datos. Esto permite cambiar el motor de BD o el framework de UI sin tocar la lógica de negocio.

---

## 4. Estructura de base de datos

### 4.1 Principio de multi-tenancy

Toda tabla del sistema lleva la columna **`EmpresaID INT NOT NULL`** como discriminador de empresa. Esto garantiza que los datos de una empresa nunca sean accesibles desde otra, a nivel de base de datos. EF Core aplica un filtro global automático por `EmpresaID` en cada consulta.

### 4.2 Soft delete (baja lógica)

Ningún registro se elimina físicamente. Todas las tablas tienen la columna **`Baja BIT NOT NULL DEFAULT 0`**. Cuando algo se "elimina" desde la UI, se marca con `Baja = 1` y desaparece de las consultas normales.

### 4.3 Tablas principales

#### Infraestructura del ecosistema

| Tabla | Propósito |
|---|---|
| `Empresas` | Catálogo de empresas registradas en la plataforma. Cada empresa tiene un `KeyConexion` que identifica su ERP legacy. |
| `Aplicaciones` | Catálogo de apps satélite del ecosistema (Ventas, Fleteros, GPS, Compras). |
| `Planes` | Planes de suscripción disponibles por aplicación. |
| `PlanCapacidades` | Límites por tipo de actor para cada plan (ej: máximo de vendedores). Tabla N:M. |
| `EmpresasPlanes` | Qué plan tiene cada empresa por aplicación. |
| `EmpresasAplicaciones` | Qué aplicaciones tiene habilitadas cada empresa. |
| `TiposActores` | Catálogo de tipos de actores del ecosistema (Vendedor, Fletero, Proveedor). |

#### Usuarios y acceso

| Tabla | Propósito |
|---|---|
| `Usuarios` | Administradores del portal web. Tienen email, contraseña hasheada, rol y empresa asignada. |
| `UsuarioAppPermisos` | Permisos granulares por usuario y aplicación (extensible). |

#### Catálogos por empresa

| Tabla | Propósito |
|---|---|
| `Vendedores` | Fuerza de ventas de cada empresa (código + nombre). |
| `Productos` | Catálogo de productos por empresa (código + nombre). |
| `Rubros` | Categorías/rubros de productos por empresa. |
| `Marcas` | Marcas de productos por empresa. |
| `Sucursales` | Sucursales/puntos de venta de cada empresa. |
| `ListasPrecios` | Listas de precios disponibles por empresa. |
| `ProductoPrecios` | Precio de cada producto por lista de precios. Tabla N:M. |

#### Clientes

| Tabla | Propósito |
|---|---|
| `Clientes` | Clientes de cada empresa. Pueden ser numéricos o alfanuméricos. |
| `ClienteVendedores` | Relación N:M entre clientes y vendedores asignados. |
| `ClienteListasPrecios` | Lista de precios principal y alternativas por cliente. |

#### Combos comerciales (módulo central)

| Tabla | Propósito |
|---|---|
| `Combos` | Combo comercial: nombre, código, vigencias, cantidades, configuración de aplicabilidad. Es la entidad más rica del sistema. |
| `ComboItems` | Productos que forman parte del combo con sus cantidades. |
| `ComboFechas` | Vigencias adicionales (fechas de inicio/fin) de un combo. |
| `ComboVendedores` | Vendedores habilitados para el combo (cuando no aplica "todos"). |
| `ComboListasPrecios` | Listas de precios aplicables al combo. |
| `ComboSucursales` | Sucursales donde aplica el combo. |
| `ComboRubros` | Rubros incluidos en la lógica de arrastre del combo. |
| `ComboMarcas` | Marcas incluidas en la lógica de arrastre del combo. |
| `ComboLogs` | Historial de cambios de cantidades en el combo (auditoría). |

#### Otros

| Tabla | Propósito |
|---|---|
| `Pedidos` | Pedidos registrados (integración con ERP legacy). |
| `GpsPosiciones` | Telemetría GPS de alta frecuencia. PK `BIGINT`. Sin soft delete. |

### 4.4 Reglas de tipos de dato

- **PK estándar:** `INT IDENTITY(1,1)` en tablas transaccionales y catálogos.
- **PK de alto volumen:** `BIGINT IDENTITY(1,1)` en tablas que crecen > 100k filas/día (GPS, logs de auditoría).

---

## 5. Secciones del portal (módulos de la UI)

### Acceso público

| Ruta | Descripción |
|---|---|
| `/login` | Pantalla de inicio de sesión. Email + contraseña. Devuelve JWT almacenado en sesión Blazor. |

### Área autenticada — todos los roles

| Ruta | Descripción |
|---|---|
| `/` | Dashboard / Inicio. Muestra resumen de la empresa activa. |
| `/combos` | Listado de combos. Permite crear, editar y dar de baja combos comerciales. |
| `/combos/nuevo` | Formulario de creación de combo. |
| `/combos/{id}` | Formulario de edición de combo. |
| `/vendedores` | Listado de vendedores de la empresa activa. CRUD completo. |
| `/productos` | Listado de productos. CRUD completo. |
| `/usuarios` | Listado de usuarios del sistema. CRUD completo. |

### Área exclusiva SuperAdmin

| Ruta | Descripción |
|---|---|
| `/empresas` | Listado de todas las empresas. Solo visible para SuperAdmin. CRUD completo. |

---

## 6. Roles y permisos

El sistema tiene **3 roles** definidos en código:

| Rol | Valor | Capacidades |
|---|---|---|
| **SuperAdmin** | 1 | Acceso total. Puede ver y gestionar **todas las empresas**, todos los usuarios, toda la configuración. Es el único que puede crear nuevas empresas en el sistema. |
| **Admin** | 2 | Acceso a todas las secciones de gestión (Combos, Vendedores, Productos, Usuarios) **dentro de su empresa asignada**. No ve el menú de Empresas. |
| **Operador** | 3 | Acceso restringido a operaciones de consulta y carga dentro de su empresa. (Permisos granulares configurables a futuro en `UsuarioAppPermisos`.) |

**Regla de acceso:** Cualquier ruta que no sea `/login` requiere estar autenticado. Si un usuario no autenticado navega a cualquier página, es redirigido automáticamente al login. Si un usuario autenticado intenta acceder a una sección sin permiso (ej: Admin intenta ir a `/empresas`), ve una pantalla de "Acceso denegado".

---

## 7. Flujo de la aplicación

```
1. Usuario navega al portal
         │
         ▼
2. ¿Tiene sesión activa (JWT válido)?
   ├── NO  → Redirige a /login
   │         Usuario ingresa email + contraseña
   │         Sistema valida credenciales y genera JWT
   │         JWT se almacena en sesión Blazor
   │         Redirige a /
   │
   └── SÍ  → Continúa navegación normal
         │
         ▼
3. Menú lateral muestra secciones según rol:
   ├── Todos: Inicio, Combos, Vendedores, Productos, Usuarios
   └── Solo SuperAdmin: + Empresas
         │
         ▼
4. Al operar (crear/editar/eliminar):
   UI Blazor → Command/Query → Handler → Repositorio → SQL Server
   Resultado → DTO → Componente Blazor actualiza vista
         │
         ▼
5. Baja lógica: ningún dato se borra físicamente.
   Se marca Baja = 1 en la tabla correspondiente.
         │
         ▼
6. Cierre de sesión: se limpia el JWT de la sesión Blazor.
   Usuario vuelve a /login.
```

---

## 8. Multi-tenancy — cómo funciona en la práctica

- Un usuario **SuperAdmin** no tiene empresa asignada (`EmpresaID = NULL`). Puede ver todo.
- Un usuario **Admin/Operador** tiene una empresa asignada. Solo ve los datos de esa empresa.
- El contexto de empresa activa (`ICurrentEmpresaContext`) se inyecta automáticamente en todos los handlers y repositorios. El desarrollador no necesita pasar el `EmpresaID` manualmente en cada consulta — EF Core lo aplica via Global Query Filter.

---

## 9. Testing

| Tipo | Proyecto | Descripción |
|---|---|---|
| Unitarios | `WebMultiempresa.UnitTests` | Tests rápidos sin dependencias externas. Cubren handlers de comandos y servicios de autenticación. |
| Integración | `WebMultiempresa.IntegrationTests` | Tests contra SQL Server real levantado en Docker con Testcontainers. Validan el schema, relaciones y operaciones completas de persistencia. |

**Nota para PM:** Los tests de integración requieren Docker corriendo en la máquina de desarrollo o en el agente de CI.

---

## 10. Stored Procedures SQL Server

El proyecto mantiene un repositorio local de SPs en la carpeta `sql-sp/` organizado por tabla. Cada SP sigue la convención de nombres:

```
<NombreTabla>_<Operacion><DescripcionPascalCase>

Ejemplos:
  Combos_TXListarPorEmpresa
  Combos_ACrear
  Combos_MActualizarVigencia
  Combos_EEliminar
```

Operaciones: `TX` (lectura), `A` (insert), `M` (update), `E` (delete/baja).

---

## 11. Convenciones de desarrollo

| Regla | Detalle |
|---|---|
| Tipos explícitos | No se usa `var` — siempre el tipo concreto (excepto tipos anónimos). |
| Idioma | Comentarios en español. Identificadores (clases, métodos, variables) en inglés. |
| Async I/O | Toda operación de base de datos o red usa `async/await`. |
| Sin exposición de entidades | La UI nunca recibe entidades de dominio directamente — siempre DTOs. |
| Inyección por constructor | No se usa service locator ni `IServiceProvider` fuera de la capa de infraestructura. |
| Nullable habilitado | Todas las referencias que pueden ser nulas están tipadas como `string?`, `int?`, etc. |

---

## 12. Estructura de carpetas del repositorio

```
WebMultiempresa/
├── src/
│   ├── WebMultiempresa.Domain/           # Entidades, enums, interfaces de dominio
│   ├── WebMultiempresa.Application/      # Casos de uso, commands, queries, DTOs, ports
│   ├── WebMultiempresa.Infrastructure/   # EF Core, repositorios, servicios JWT, seeder
│   └── WebMultiempresa.Blazor/           # Páginas Razor, layouts, auth provider, Program.cs
├── tests/
│   ├── WebMultiempresa.UnitTests/        # Tests sin dependencias externas
│   └── WebMultiempresa.IntegrationTests/ # Tests con DB real (Testcontainers)
├── sql-sp/                               # Scripts SQL de stored procedures por tabla
├── sql-ddl/                              # Scripts DDL de tablas
└── docs/                                 # Documentación del proyecto
```

---

## 13. Diagrama entidad-relación (simplificado)

```
Empresas ──┬── Usuarios (Admin/Operador de esa empresa)
           ├── Vendedores
           ├── Productos ──── ProductoPrecios ──── ListasPrecios
           ├── Rubros
           ├── Marcas
           ├── Sucursales
           ├── Clientes ──── ClienteVendedores ──── Vendedores
           │              └── ClienteListasPrecios ── ListasPrecios
           └── Combos ─────── ComboItems ────────── Productos
                          ├── ComboFechas
                          ├── ComboVendedores ────── Vendedores
                          ├── ComboListasPrecios ─── ListasPrecios
                          ├── ComboSucursales ────── Sucursales
                          ├── ComboRubros ────────── Rubros
                          ├── ComboMarcas ────────── Marcas
                          └── ComboLogs

Aplicaciones ─── Planes ─── PlanCapacidades ─── TiposActores
           └──── EmpresasAplicaciones ─── Empresas
           └──── EmpresasPlanes ─────────── Empresas
```

---

## 14. Decisiones de diseño relevantes para el roadmap

| Decisión | Implicancia para el PM |
|---|---|
| **Multi-tenancy por columna** | Agregar una empresa nueva es un `INSERT` en `Empresas`. No requiere nueva instancia ni nueva base de datos. |
| **Soft delete universal** | Los datos "eliminados" siguen existiendo. Se pueden recuperar. Los reportes históricos siguen siendo válidos. |
| **Arquitectura hexagonal** | Cambiar de SQL Server a otro motor de BD, o de Blazor a React, no requiere reescribir la lógica de negocio. |
| **JWT sin refresh token (v1)** | La sesión expira al cerrar el browser. No hay "recordarme". Esto es intencional en v1 por simplicidad. |
| **Tablas legacy ERP** | El sistema puede leer datos del ERP existente (mapeados como `ExcludeFromMigrations`). No escribe en tablas legacy excepto las de combos que las referencian por FK. |
| **Combos como entidad central** | El módulo de Combos es el más complejo: tiene 10 tablas relacionadas, lógica de arrastre, vigencias múltiples, segmentación por vendedor/lista/sucursal/cliente. Es la funcionalidad de mayor valor del sistema. |

---

*Documento generado: Mayo 2026*