using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class MarcaProductoRepository : IMarcaProductoRepository
{
    private readonly AppDbContext _dbContext;

    public MarcaProductoRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<MarcaProducto>> ListarActivosAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.MarcasProductos
            .AsNoTracking()
            .Where(r => !r.Baja)
            .OrderBy(r => r.Nombre)
            .ToListAsync(cancellationToken);
    }
}
