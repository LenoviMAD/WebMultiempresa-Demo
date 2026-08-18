using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _dbContext;

    public ProductoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Producto>> ListarActivosAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Productos
            .AsNoTracking()
            .Where(p => !p.Baja)
            .OrderBy(p => p.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<Producto?> ObtenerPorIdAsync(int productosId, CancellationToken cancellationToken)
    {
        return await _dbContext.Productos
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductosID == productosId, cancellationToken);
    }

    public async Task<IReadOnlyList<(int ListasPreciosID, string NombreLista, decimal PrecioFinal)>> ObtenerPreciosAsync(
        int productosId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.ProductoPrecios
            .AsNoTracking()
            .Where(pp => pp.ProductosID == productosId && !pp.Baja)
            .Join(
                _dbContext.ListasPrecios.Where(l => !l.Baja),
                pp => pp.ListasPreciosID,
                l => l.ListasPreciosID,
                (pp, l) => new { l.ListasPreciosID, l.Nombre, pp.PrecioFinal })
            .OrderBy(x => x.Nombre)
            .Select(x => new ValueTuple<int, string, decimal>(x.ListasPreciosID, x.Nombre, x.PrecioFinal))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyDictionary<int, DateTime>> ObtenerFechasUltimoPrecioAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ProductoPrecios
            .AsNoTracking()
            .Where(pp => !pp.Baja)
            .GroupBy(pp => pp.ProductosID)
            .Select(g => new { ProductosID = g.Key, UltimaFecha = g.Max(pp => pp.FechaActualizacion) })
            .ToDictionaryAsync(x => x.ProductosID, x => x.UltimaFecha, cancellationToken);
    }

    public async Task<bool> ExisteCodigoAsync(int empresaId, string codigo, int? excludeProductosID, CancellationToken cancellationToken)
    {
        return await _dbContext.Productos
            .AsNoTracking()
            .IgnoreQueryFilters()
            .AnyAsync(p => p.EmpresaID == empresaId
                        && p.Codigo == codigo
                        && !p.Baja
                        && (excludeProductosID == null || p.ProductosID != excludeProductosID),
                      cancellationToken);
    }

    public async Task<int> CrearAsync(Producto producto, CancellationToken cancellationToken)
    {
        _dbContext.Productos.Add(producto);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return producto.ProductosID;
    }

    public async Task ActualizarAsync(Producto producto, CancellationToken cancellationToken)
    {
        _dbContext.Productos.Update(producto);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BajaLogicaAsync(int productosId, CancellationToken cancellationToken)
    {
        await _dbContext.Productos
            .Where(p => p.ProductosID == productosId)
            .ExecuteUpdateAsync(s => s.SetProperty(p => p.Baja, true), cancellationToken);
    }
}
