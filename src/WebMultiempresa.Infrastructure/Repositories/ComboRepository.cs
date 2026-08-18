using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;
using System.Collections.Generic;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class ComboRepository : IComboRepository
{
    private readonly AppDbContext _dbContext;

    public ComboRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExisteCodigoAsync(int empresaId, string codigo, int? excludeCombosID, CancellationToken cancellationToken)
    {
        return await _dbContext.Combos
            .AsNoTracking()
            .IgnoreQueryFilters()
            .AnyAsync(c => c.EmpresaID == empresaId
                        && c.Codigo == codigo
                        && !c.Baja
                        && (excludeCombosID == null || c.CombosID != excludeCombosID),
                      cancellationToken);
    }

    public async Task<IReadOnlyList<Combo>> ListarActivosAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Combos
            .AsNoTracking()
            .Include(c => c.MarcaProducto)
            .Where(c => !c.Baja)
            .OrderBy(c => c.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<Combo?> ObtenerPorIdAsync(int combosId, CancellationToken cancellationToken)
    {
        return await _dbContext.Combos
            .AsNoTracking()
            .Include(c => c.MarcaProducto)
            .Include(c => c.Items)
                .ThenInclude(i => i.Producto)
            .Include(c => c.Fechas)
            .Include(c => c.Vendedores)
            .Include(c => c.ListasPrecios)
            .Include(c => c.Sucursales)
            .Include(c => c.MarcasProductosArrastre)
                .ThenInclude(r => r.MarcaProducto)
            .Include(c => c.FamiliaProductosArrastre)
                .ThenInclude(m => m.FamiliaProducto)
            .Include(c => c.Logs)
            .FirstOrDefaultAsync(c => c.CombosID == combosId, cancellationToken);
    }

    public async Task<int> CrearAsync(Combo combo, CancellationToken cancellationToken)
    {
        _dbContext.Combos.Add(combo);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return combo.CombosID;
    }

    public async Task ActualizarAsync(Combo combo, CancellationToken cancellationToken)
    {
        _dbContext.Combos.Update(combo);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BajaLogicaAsync(int combosId, CancellationToken cancellationToken)
    {
        await _dbContext.Combos
            .Where(c => c.CombosID == combosId)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.Baja, true), cancellationToken);
    }

    public async Task ActualizarRelacionesAsync(
        int combosId,
        IReadOnlyList<int> vendedoresIds,
        IReadOnlyList<int> listasPreciosIds,
        IReadOnlyList<int> sucursalesIds,
        CancellationToken cancellationToken)
    {
        await _dbContext.ComboVendedores
            .Where(v => v.CombosID == combosId)
            .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.ComboListasPrecios
            .Where(l => l.CombosID == combosId)
            .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.ComboSucursales
            .Where(s => s.CombosID == combosId)
            .ExecuteDeleteAsync(cancellationToken);

        if (vendedoresIds.Count > 0)
            _dbContext.ComboVendedores.AddRange(
                vendedoresIds.Select(id => ComboVendedor.Crear(combosId, id)));

        if (listasPreciosIds.Count > 0)
            _dbContext.ComboListasPrecios.AddRange(
                listasPreciosIds.Select(id => ComboListaPrecios.Crear(combosId, id)));

        if (sucursalesIds.Count > 0)
            _dbContext.ComboSucursales.AddRange(
                sucursalesIds.Select(id => ComboSucursal.Crear(combosId, id)));

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarArrastreAsync(
        int combosId,
        IReadOnlyList<int> marcasProductosIds,
        IReadOnlyList<int> familiaProductosIds,
        CancellationToken cancellationToken)
    {
        await _dbContext.ComboMarcasProductos
            .Where(r => r.CombosID == combosId)
            .ExecuteDeleteAsync(cancellationToken);

        await _dbContext.ComboFamiliaProductos
            .Where(m => m.CombosID == combosId)
            .ExecuteDeleteAsync(cancellationToken);

        if (marcasProductosIds.Count > 0)
            _dbContext.ComboMarcasProductos.AddRange(
                marcasProductosIds.Select(id => ComboMarcaProducto.Crear(combosId, id)));

        if (familiaProductosIds.Count > 0)
            _dbContext.ComboFamiliaProductos.AddRange(
                familiaProductosIds.Select(id => ComboFamiliaProducto.Crear(combosId, id)));

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AgregarLogAsync(ComboLog log, CancellationToken cancellationToken)
    {
        _dbContext.ComboLogs.Add(log);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AgregarVigenciasAsync(
        DateTime fechaInicio,
        DateTime fechaFin,
        IReadOnlyList<int> combosIds,
        CancellationToken cancellationToken)
    {
        _dbContext.ComboFechas.AddRange(
            combosIds.Select(id => ComboFecha.Crear(id, fechaInicio, fechaFin)));

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
