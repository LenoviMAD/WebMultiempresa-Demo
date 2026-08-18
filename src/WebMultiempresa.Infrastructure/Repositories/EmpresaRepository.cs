using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class EmpresaRepository : IEmpresaRepository
{
    private readonly AppDbContext _dbContext;

    public EmpresaRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Empresa?> ObtenerPorIdAsync(int empresaId, CancellationToken cancellationToken)
    {
        return await _dbContext.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmpresaID == empresaId, cancellationToken);
    }

    public async Task<IReadOnlyList<Empresa>> ListarActivasAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Empresas
            .AsNoTracking()
            .Where(e => !e.Baja)
            .OrderBy(e => e.Nombre)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExisteNombreAsync(string nombre, int? excludeEmpresaID, CancellationToken cancellationToken)
    {
        return await _dbContext.Empresas
            .AsNoTracking()
            .AnyAsync(e => e.Nombre == nombre.Trim()
                        && !e.Baja
                        && (excludeEmpresaID == null || e.EmpresaID != excludeEmpresaID),
                      cancellationToken);
    }

    public async Task<int> CrearAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        _dbContext.Empresas.Add(empresa);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return empresa.EmpresaID;
    }

    public async Task ActualizarAsync(Empresa empresa, CancellationToken cancellationToken)
    {
        _dbContext.Empresas.Update(empresa);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BajaLogicaAsync(int empresaId, CancellationToken cancellationToken)
    {
        await _dbContext.Empresas
            .Where(e => e.EmpresaID == empresaId)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.Baja, true), cancellationToken);
    }
}
