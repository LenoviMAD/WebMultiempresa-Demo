using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

internal sealed class VendedorEstrellasDefinicionRepository : IVendedorEstrellasDefinicionRepository
{
    private readonly AppDbContext _db;

    public VendedorEstrellasDefinicionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<VendedorEstrellasDefinicion>> ListarActivasAsync(CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasDefiniciones
            .AsNoTracking()
            .Where(d => !d.Baja)
            .OrderBy(d => d.NumeroEstrella)
            .ToListAsync(cancellationToken);

    public async Task<VendedorEstrellasDefinicion?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasDefiniciones
            .FirstOrDefaultAsync(d => d.VendedorEstrellasDefinicionesID == id, cancellationToken);

    public async Task<byte> ObtenerSiguienteNumeroAsync(int empresaId, CancellationToken cancellationToken)
    {
        // Incluye filas con Baja=true para nunca reutilizar un número ya asignado
        int? max = await _db.VendedorEstrellasDefiniciones
            .IgnoreQueryFilters()
            .Where(d => d.EmpresaID == empresaId)
            .Select(d => (int?)d.NumeroEstrella)
            .MaxAsync(cancellationToken);
        return (byte)((max ?? 0) + 1);
    }

    public async Task<bool> ExisteNumeroAsync(byte numero, int empresaId, int? excludeId, CancellationToken cancellationToken) =>
        await _db.VendedorEstrellasDefiniciones
            .AnyAsync(d =>
                d.NumeroEstrella == numero &&
                d.EmpresaID == empresaId &&
                !d.Baja &&
                (excludeId == null || d.VendedorEstrellasDefinicionesID != excludeId),
                cancellationToken);

    public async Task AgregarAsync(VendedorEstrellasDefinicion definicion, CancellationToken cancellationToken)
    {
        await _db.VendedorEstrellasDefiniciones.AddAsync(definicion, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(VendedorEstrellasDefinicion definicion, CancellationToken cancellationToken)
    {
        _db.VendedorEstrellasDefiniciones.Update(definicion);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
