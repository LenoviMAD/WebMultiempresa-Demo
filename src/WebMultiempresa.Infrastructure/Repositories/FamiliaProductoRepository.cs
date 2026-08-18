using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class FamiliaProductoRepository : IFamiliaProductoRepository
{
    private readonly AppDbContext _dbContext;

    public FamiliaProductoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<FamiliaProducto>> ListarActivasAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.FamiliaProductos
            .AsNoTracking()
            .Where(m => !m.Baja)
            .OrderBy(m => m.Nombre)
            .ToListAsync(cancellationToken);
    }
}
