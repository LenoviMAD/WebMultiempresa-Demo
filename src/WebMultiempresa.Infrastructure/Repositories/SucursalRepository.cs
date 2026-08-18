using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class SucursalRepository : ISucursalRepository
{
    private readonly AppDbContext _dbContext;

    public SucursalRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Sucursal>> ListarActivasAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Sucursales
            .AsNoTracking()
            .Where(s => !s.Baja)
            .OrderBy(s => s.Nombre)
            .ToListAsync(cancellationToken);
    }
}
