using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class ListaPreciosRepository : IListaPreciosRepository
{
    private readonly AppDbContext _dbContext;

    public ListaPreciosRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ListaPrecios>> ListarActivasAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ListasPrecios
            .AsNoTracking()
            .Where(l => !l.Baja)
            .OrderBy(l => l.Nombre)
            .ToListAsync(cancellationToken);
    }
}
