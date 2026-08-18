using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

public sealed class VendedorEstrellaDiariaRepository(AppDbContext dbContext)
    : IVendedorEstrellaDiariaRepository
{
    public async Task<IReadOnlyList<VendedorEstrellaDiaria>> ListarPorVendedorYPeriodoAsync(
        int vendedoresId,
        DateTime fechaDesde,
        DateTime fechaHasta,
        CancellationToken cancellationToken) =>
        await dbContext.VendedorEstrellasDiarias
            .AsNoTracking()
            .Where(e => e.VendedoresID == vendedoresId
                     && e.Fecha        >= fechaDesde.Date
                     && e.Fecha        <= fechaHasta.Date
                     && !e.Baja)
            .OrderBy(e => e.Fecha)
            .ThenBy(e => e.VendedorEstrellasDefinicionesID)
            .ToListAsync(cancellationToken);
}
