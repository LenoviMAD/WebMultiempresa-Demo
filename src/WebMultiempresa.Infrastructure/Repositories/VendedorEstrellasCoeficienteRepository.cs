using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

internal sealed class VendedorEstrellasCoeficienteRepository : IVendedorEstrellasCoeficienteRepository
{
    private readonly AppDbContext _db;

    public VendedorEstrellasCoeficienteRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<VendedorEstrellasCoeficiente>> ListarActivosAsync(CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasCoeficientes
            .AsNoTracking()
            .Where(c => !c.Baja)
            .OrderBy(c => c.CantidadEstrellas)
            .ToListAsync(cancellationToken);

    public async Task<VendedorEstrellasCoeficiente?> ObtenerPorCantidadAsync(byte cantidad, int empresaId, CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasCoeficientes
            .FirstOrDefaultAsync(c => c.CantidadEstrellas == cantidad && c.EmpresaID == empresaId, cancellationToken);

    public async Task<VendedorEstrellasCoeficiente?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasCoeficientes
            .FirstOrDefaultAsync(c => c.VendedorEstrellasCoeficientesID == id, cancellationToken);

    public async Task AgregarAsync(VendedorEstrellasCoeficiente coeficiente, CancellationToken cancellationToken)
    {
        await _db.VendedorEstrellasCoeficientes.AddAsync(coeficiente, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(VendedorEstrellasCoeficiente coeficiente, CancellationToken cancellationToken)
    {
        _db.VendedorEstrellasCoeficientes.Update(coeficiente);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
