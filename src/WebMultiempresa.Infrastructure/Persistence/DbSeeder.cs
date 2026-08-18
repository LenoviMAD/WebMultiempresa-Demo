using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Enums;

namespace WebMultiempresa.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        await using AsyncServiceScope scope = services.CreateAsyncScope();

        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        IPasswordHasher hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        ILogger<AppDbContext> logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        await db.Database.MigrateAsync();

        Empresa? empresa = await db.Set<Empresa>().FirstOrDefaultAsync();

        if (empresa is null)
        {
            empresa = Empresa.Crear("Empresa Demo", "DEMO");
            db.Set<Empresa>().Add(empresa);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: empresa '{Nombre}' creada (ID={Id}).", empresa.Nombre, empresa.EmpresaID);

            // Admin de empresa (Rol = 1)
            string hashAdmin = hasher.Hashear("Admin123!");
            Usuario adminEmpresa = Usuario.Crear(empresa.EmpresaID, "admin@demo.com", hashAdmin, "Administrador Demo", rol: 1);
            db.Set<Usuario>().Add(adminEmpresa);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: usuario 'admin@demo.com' creado.");

            // Operador adicional de la empresa (Rol = 1)
            string hashOperador = hasher.Hashear("Operador123!");
            Usuario operadorEmpresa = Usuario.Crear(empresa.EmpresaID, "operador@demo.com", hashOperador, "Operador Demo", rol: 1);
            db.Set<Usuario>().Add(operadorEmpresa);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: usuario 'operador@demo.com' creado.");
        }

        if (!await db.Set<Usuario>().AnyAsync(u => u.Rol == 0))
        {
            string hashSuper = hasher.Hashear("Super123!");
            Usuario superAdmin = Usuario.Crear(null, "super@admin.com", hashSuper, "Super Admin", rol: 0);
            db.Set<Usuario>().Add(superAdmin);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: superadmin 'super@admin.com' creado.");
        }

        // Seed: Aplicacion App Ventas + TipoActor Vendedor (necesario para ActorEstadoLog)
        if (!await db.Set<Aplicacion>().AnyAsync(a => a.Nombre == "App Ventas"))
        {
            Aplicacion appVentas = Aplicacion.Crear("App Ventas", "Aplicación de vendedores de campo");
            db.Set<Aplicacion>().Add(appVentas);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: aplicación 'App Ventas' creada (ID={Id}).", appVentas.AplicacionesID);

            TipoActor tipoVendedor = TipoActor.Crear(appVentas.AplicacionesID, "Vendedor", "VENDEDOR");
            db.Set<TipoActor>().Add(tipoVendedor);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: TipoActor 'VENDEDOR' creado (ID={Id}).", tipoVendedor.TiposActoresID);
        }

        // Seed: Lista de precios general (requerida por Clientes)
        ListaPrecios? listaGeneral = await db.Set<ListaPrecios>().FirstOrDefaultAsync(l => l.EmpresaID == empresa.EmpresaID);
        if (listaGeneral is null)
        {
            listaGeneral = ListaPrecios.Crear(empresa.EmpresaID, "Lista General", 0m);
            db.Set<ListaPrecios>().Add(listaGeneral);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: lista de precios '{Nombre}' creada (ID={Id}).", listaGeneral.Nombre, listaGeneral.ListasPreciosID);
        }

        // Seed: Productos de ejemplo
        if (!await db.Set<Producto>().AnyAsync(p => p.EmpresaID == empresa.EmpresaID))
        {
            (string codigo, string nombre)[] productosDemo =
            [
                ("PROD-001", "Gaseosa Cola 2.25L"),
                ("PROD-002", "Agua Mineral 1.5L"),
                ("PROD-003", "Jugo de Naranja 1L"),
                ("PROD-004", "Cerveza Rubia Pack x6"),
                ("PROD-005", "Fideos Tallarín 500g"),
                ("PROD-006", "Arroz Largo Fino 1Kg"),
                ("PROD-007", "Aceite de Girasol 900ml"),
                ("PROD-008", "Yerba Mate 1Kg"),
                ("PROD-009", "Galletitas Dulces 300g"),
                ("PROD-010", "Café Molido 250g")
            ];

            foreach ((string codigo, string nombre) in productosDemo)
                db.Set<Producto>().Add(Producto.Crear(empresa.EmpresaID, codigo, nombre));

            await db.SaveChangesAsync();
            logger.LogInformation("Seed: {Cantidad} productos demo creados.", productosDemo.Length);
        }

        // Seed: Vendedores de ejemplo
        if (!await db.Set<Vendedor>().AnyAsync(v => v.EmpresaID == empresa.EmpresaID))
        {
            Vendedor vendedor1 = Vendedor.Crear(empresa.EmpresaID, "VEND-001", "Juan Pérez", whatsApp: "+541100000001");
            Vendedor vendedor2 = Vendedor.Crear(empresa.EmpresaID, "VEND-002", "María López", whatsApp: "+541100000002");
            db.Set<Vendedor>().AddRange(vendedor1, vendedor2);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: 2 vendedores demo creados.");
        }

        // Seed: Clientes de ejemplo
        if (!await db.Set<Cliente>().AnyAsync(c => c.EmpresaID == empresa.EmpresaID))
        {
            Cliente cliente1 = Cliente.Crear(empresa.EmpresaID, "CLI-001", "Almacén Don José", diaDeVenta: 1,
                listaDePrecioId: listaGeneral.ListasPreciosID, consumidorFinal: false,
                direccion: "Av. Siempre Viva 123", localidad: "Ciudad Demo", provincia: "Buenos Aires",
                telefono: "+541100000101", email: "donjose@demo.com");
            Cliente cliente2 = Cliente.Crear(empresa.EmpresaID, "CLI-002", "Kiosco La Esquina", diaDeVenta: 2,
                listaDePrecioId: listaGeneral.ListasPreciosID, consumidorFinal: false,
                direccion: "Calle Falsa 456", localidad: "Ciudad Demo", provincia: "Buenos Aires",
                telefono: "+541100000102", email: "laesquina@demo.com");
            Cliente cliente3 = Cliente.Crear(empresa.EmpresaID, "CLI-003", "Supermercado Central", diaDeVenta: 3,
                listaDePrecioId: listaGeneral.ListasPreciosID, consumidorFinal: false,
                direccion: "Ruta 8 Km 45", localidad: "Ciudad Demo", provincia: "Buenos Aires",
                telefono: "+541100000103", email: "central@demo.com");
            db.Set<Cliente>().AddRange(cliente1, cliente2, cliente3);
            await db.SaveChangesAsync();
            logger.LogInformation("Seed: 3 clientes demo creados.");
        }
    }
}
