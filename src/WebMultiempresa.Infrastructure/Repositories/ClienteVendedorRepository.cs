using Microsoft.EntityFrameworkCore;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Repositories;

internal sealed class ClienteVendedorRepository : IClienteVendedorRepository
{
    private readonly AppDbContext _db;

    public ClienteVendedorRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ClienteVendedor>> ListarActivosPorVendedorAsync(
        int vendedoresId,
        CancellationToken cancellationToken) =>
        await _db.ClienteVendedores
            .AsNoTracking()
            .Where(cv => cv.VendedoresID == vendedoresId && !cv.Baja)
            .ToListAsync(cancellationToken);

    public async Task<ClienteVendedor?> ObtenerAsync(
        int clientesId,
        int vendedoresId,
        CancellationToken cancellationToken) =>
        // Sin filtro por Baja: permite detectar registros dados de baja para reactivarlos
        // y evitar violación de UNIQUE(ClientesID, VendedoresID) al reinsertar
        await _db.ClienteVendedores
            .FirstOrDefaultAsync(
                cv => cv.ClientesID == clientesId && cv.VendedoresID == vendedoresId,
                cancellationToken);

    public async Task AgregarAsync(ClienteVendedor clienteVendedor, CancellationToken cancellationToken)
    {
        await _db.ClienteVendedores.AddAsync(clienteVendedor, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task AgregarRangoAsync(
        IEnumerable<ClienteVendedor> clienteVendedores,
        CancellationToken cancellationToken)
    {
        await _db.ClienteVendedores.AddRangeAsync(clienteVendedores, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(ClienteVendedor clienteVendedor, CancellationToken cancellationToken)
    {
        _db.ClienteVendedores.Update(clienteVendedor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Cliente>> ListarClientesDiaSiguienteAsync(
        int vendedoresId,
        int diaSemana,
        CancellationToken cancellationToken) =>
        await _db.ClienteVendedores
            .AsNoTracking()
            .Where(cv => cv.VendedoresID == vendedoresId && !cv.Baja)
            .Join(
                _db.Clientes.Where(c => c.DiaDeVenta == diaSemana && !c.Baja),
                cv => cv.ClientesID,
                c  => c.ClientesID,
                (cv, c) => c)
            .OrderBy(c => c.Nombre)
            .ToListAsync(cancellationToken);
}
