using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class VendedorRepository : IVendedorRepository
{
    private readonly AppDbContext _dbContext;

    public VendedorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Vendedor>> ListarActivosAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Vendedores
            .AsNoTracking()
            .Where(v => !v.Baja)
            .OrderBy(v => v.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Vendedor>> ListarDadosDeBajaAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Vendedores
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(v => v.Baja)
            .OrderBy(v => v.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<Vendedor?> ObtenerPorIdAsync(int vendedoresId, CancellationToken cancellationToken)
    {
        return await _dbContext.Vendedores
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(v => v.VendedoresID == vendedoresId, cancellationToken);
    }

    public async Task<bool> ExisteCodigoAsync(
        string codigo,
        int empresaId,
        int? excludeVendedoresId,
        CancellationToken cancellationToken)
    {
        string codigoNormalizado = codigo.Trim().ToUpperInvariant();
        return await _dbContext.Vendedores
            .IgnoreQueryFilters()
            .AnyAsync(v =>
                v.EmpresaID == empresaId &&
                v.Codigo == codigoNormalizado &&
                (excludeVendedoresId == null || v.VendedoresID != excludeVendedoresId),
                cancellationToken);
    }

    public async Task<Vendedor?> ObtenerPorCodigoAsync(
        string codigo,
        int empresaId,
        CancellationToken cancellationToken)
    {
        string codigoNormalizado = codigo.Trim().ToUpperInvariant();
        return await _dbContext.Vendedores
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(
                v => v.Codigo == codigoNormalizado && v.EmpresaID == empresaId && !v.Baja,
                cancellationToken);
    }

    public async Task AgregarAsync(Vendedor vendedor, CancellationToken cancellationToken)
    {
        await _dbContext.Vendedores.AddAsync(vendedor, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(Vendedor vendedor, CancellationToken cancellationToken)
    {
        _dbContext.Vendedores.Update(vendedor);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyDictionary<int, IReadOnlySet<int>>> ObtenerEstrellasEncendidasPorVendedorAsync(CancellationToken cancellationToken)
    {
        // Obtener la última fecha con estrellas registradas por vendedor (Global Query Filter aplica EmpresaID)
        List<VendedorEstrellaDiaria> todasLasEstrellas = await _dbContext.VendedorEstrellasDiarias
            .AsNoTracking()
            .Where(ed => !ed.Baja)
            .OrderByDescending(ed => ed.Fecha)
            .ToListAsync(cancellationToken);

        // Quedarse solo con las estrellas del último día de cada vendedor
        IEnumerable<VendedorEstrellaDiaria> encendidas = todasLasEstrellas
            .GroupBy(ed => ed.VendedoresID)
            .SelectMany(g =>
            {
                DateTime ultimaFecha = g.Max(ed => ed.Fecha);
                return g.Where(ed => ed.Fecha == ultimaFecha && ed.EstaEncendida);
            });

        return encendidas
            .GroupBy(ed => ed.VendedoresID)
            .ToDictionary(
                g => g.Key,
                g => (IReadOnlySet<int>)g.Select(ed => ed.VendedorEstrellasDefinicionesID).ToHashSet());
    }
}
