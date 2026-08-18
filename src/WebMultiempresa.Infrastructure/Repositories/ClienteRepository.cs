using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

internal sealed class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _db;

    public ClienteRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Cliente>> ListarActivosAsync(CancellationToken cancellationToken) =>
        await _db.Clientes
            .AsNoTracking()
            .Where(c => !c.Baja)
            .OrderBy(c => c.Nombre)
            .ToListAsync(cancellationToken);
}
