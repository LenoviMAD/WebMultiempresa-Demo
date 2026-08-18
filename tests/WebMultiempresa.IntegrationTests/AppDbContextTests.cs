using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using WebMultiempresa.Infrastructure.Persistence;
using Xunit;

namespace WebMultiempresa.IntegrationTests;

/// <summary>
/// Verifica que las migraciones se aplican correctamente y que el
/// Global Query Filter aísla los datos por empresa.
/// </summary>
public sealed class AppDbContextTests : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public async Task InitializeAsync() => await _container.StartAsync();

    public async Task DisposeAsync() => await _container.DisposeAsync();

    // ── helpers ──────────────────────────────────────────────────────────────

    private AppDbContext CrearContexto(int? empresaId)
    {
        EmpresaContexto empresaContexto = new() { EmpresaID = empresaId };

        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_container.GetConnectionString())
            .Options;

        return new AppDbContext(options, empresaContexto);
    }

    // ── tests ─────────────────────────────────────────────────────────────────

    [Fact]
    public async Task Migrations_SeAplican_Correctamente()
    {
        await using AppDbContext context = CrearContexto(empresaId: null);

        await context.Database.MigrateAsync();
        bool conectado = await context.Database.CanConnectAsync();

        Assert.True(conectado);
    }

    [Fact]
    public async Task GlobalQueryFilter_AislaRubrosPorEmpresa()
    {
        await using AppDbContext setup = CrearContexto(empresaId: null);
        await setup.Database.MigrateAsync();

        // Insertar dos empresas y un rubro por empresa via SQL directo
        // (entidades con constructor privado — no se pueden instanciar en tests)
        await setup.Database.ExecuteSqlRawAsync(@"
            INSERT INTO Empresas (Nombre, KeyConexion, Baja)
            VALUES ('Empresa A', 'key-a', 0), ('Empresa B', 'key-b', 0);

            DECLARE @IdA INT = (SELECT EmpresaID FROM Empresas WHERE KeyConexion = 'key-a');
            DECLARE @IdB INT = (SELECT EmpresaID FROM Empresas WHERE KeyConexion = 'key-b');

            INSERT INTO Rubros (EmpresaID, Nombre, Baja)
            VALUES (@IdA, 'Rubro de A', 0), (@IdB, 'Rubro de B', 0);
        ");

        int empresaAId = await setup.Empresas
            .Where(e => e.KeyConexion == "key-a")
            .Select(e => e.EmpresaID)
            .FirstAsync();

        int empresaBId = await setup.Empresas
            .Where(e => e.KeyConexion == "key-b")
            .Select(e => e.EmpresaID)
            .FirstAsync();

        // Consultar con el filtro activo para Empresa A — solo debe ver su rubro
        await using AppDbContext contextoA = CrearContexto(empresaAId);
        List<Domain.Entities.MarcaProducto> rubrosA = await contextoA.MarcasProductos
            .AsNoTracking()
            .ToListAsync();

        Assert.Single(rubrosA);
        Assert.Equal("Rubro de A", rubrosA[0].Nombre);

        // Consultar con el filtro activo para Empresa B — solo debe ver su rubro
        await using AppDbContext contextoB = CrearContexto(empresaBId);
        List<Domain.Entities.MarcaProducto> rubrosB = await contextoB.MarcasProductos
            .AsNoTracking()
            .ToListAsync();

        Assert.Single(rubrosB);
        Assert.Equal("Rubro de B", rubrosB[0].Nombre);
    }

    [Fact]
    public async Task GlobalQueryFilter_SuperAdmin_VeTodosLosRubros()
    {
        await using AppDbContext setup = CrearContexto(empresaId: null);
        await setup.Database.MigrateAsync();

        await setup.Database.ExecuteSqlRawAsync(@"
            INSERT INTO Empresas (Nombre, KeyConexion, Baja)
            VALUES ('Empresa X', 'key-x', 0), ('Empresa Y', 'key-y', 0);

            DECLARE @IdX INT = (SELECT EmpresaID FROM Empresas WHERE KeyConexion = 'key-x');
            DECLARE @IdY INT = (SELECT EmpresaID FROM Empresas WHERE KeyConexion = 'key-y');

            INSERT INTO Rubros (EmpresaID, Nombre, Baja)
            VALUES (@IdX, 'Rubro X', 0), (@IdY, 'Rubro Y', 0);
        ");

        // SuperAdmin (EmpresaID = null) — el filtro no aplica, ve todos
        await using AppDbContext superAdmin = CrearContexto(empresaId: null);
        List<Domain.Entities.MarcaProducto> todos = await superAdmin.MarcasProductos
            .AsNoTracking()
            .ToListAsync();

        Assert.Equal(2, todos.Count);
    }
}
