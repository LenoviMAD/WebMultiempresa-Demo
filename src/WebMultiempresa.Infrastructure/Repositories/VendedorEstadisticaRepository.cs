using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class VendedorEstadisticaRepository(AppDbContext dbContext)
    : IVendedorEstadisticaRepository
{
    public async Task<VendedorEstadistica?> ObtenerMasRecienteAsync(
        int vendedoresId,
        CancellationToken cancellationToken) =>
        await dbContext.VendedorEstadisticas
            .AsNoTracking()
            .Where(e => e.VendedoresID == vendedoresId && !e.Baja)
            .OrderByDescending(e => e.Fecha)
            .FirstOrDefaultAsync(cancellationToken);
}
