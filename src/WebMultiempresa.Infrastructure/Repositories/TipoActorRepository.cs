using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class TipoActorRepository : ITipoActorRepository
{
    private readonly AppDbContext _dbContext;

    public TipoActorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TipoActor?> ObtenerPorCodigoAsync(string codigo, CancellationToken cancellationToken)
    {
        string codigoNormalizado = codigo.Trim().ToUpperInvariant();
        return await _dbContext.TiposActores
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Codigo == codigoNormalizado && !t.Baja, cancellationToken);
    }
}
