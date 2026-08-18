using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

internal sealed class CategoriaComercialRepository : ICategoriaComercialRepository
{
    private readonly AppDbContext _db;

    public CategoriaComercialRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<CategoriaComercial>> ListarActivasAsync(CancellationToken cancellationToken) =>
        await _db.CategoriasComerciales
            .AsNoTracking()
            .Where(r => !r.Baja)
            .OrderBy(r => r.Nombre)
            .ToListAsync(cancellationToken);
}
