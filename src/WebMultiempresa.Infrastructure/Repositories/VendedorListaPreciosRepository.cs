using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class VendedorListaPreciosRepository : IVendedorListaPreciosRepository
{
    private readonly AppDbContext _dbContext;

    public VendedorListaPreciosRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<VendedorListaPrecios>> ListarActivasPorVendedorAsync(
        int vendedoresId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.VendedorListasPrecios
            .AsNoTracking()
            .Where(v => v.VendedoresID == vendedoresId && !v.Baja)
            .OrderByDescending(v => v.EsDefault)
            .ToListAsync(cancellationToken);
    }

    public async Task<VendedorListaPrecios?> ObtenerPorIdAsync(
        int vendedorListasPreciosId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.VendedorListasPrecios
            .FirstOrDefaultAsync(v => v.VendedorListasPreciosID == vendedorListasPreciosId, cancellationToken);
    }

    public async Task<VendedorListaPrecios?> ObtenerDefaultActivaAsync(
        int vendedoresId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.VendedorListasPrecios
            .FirstOrDefaultAsync(
                v => v.VendedoresID == vendedoresId && v.EsDefault && !v.Baja,
                cancellationToken);
    }

    public async Task<bool> ExisteAsignacionActivaAsync(
        int vendedoresId,
        int listaPreciosId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.VendedorListasPrecios
            .AnyAsync(
                v => v.VendedoresID == vendedoresId && v.ListasPreciosID == listaPreciosId && !v.Baja,
                cancellationToken);
    }

    public async Task AgregarAsync(VendedorListaPrecios lista, CancellationToken cancellationToken)
    {
        await _dbContext.VendedorListasPrecios.AddAsync(lista, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(VendedorListaPrecios lista, CancellationToken cancellationToken)
    {
        _dbContext.VendedorListasPrecios.Update(lista);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
