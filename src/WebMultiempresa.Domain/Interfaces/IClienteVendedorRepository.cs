using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IClienteVendedorRepository
{
    Task<IReadOnlyList<ClienteVendedor>> ListarActivosPorVendedorAsync(int vendedoresId, CancellationToken cancellationToken);
    Task<ClienteVendedor?> ObtenerAsync(int clientesId, int vendedoresId, CancellationToken cancellationToken);
    Task AgregarAsync(ClienteVendedor clienteVendedor, CancellationToken cancellationToken);
    Task AgregarRangoAsync(IEnumerable<ClienteVendedor> clienteVendedores, CancellationToken cancellationToken);
    Task ActualizarAsync(ClienteVendedor clienteVendedor, CancellationToken cancellationToken);
    Task<IReadOnlyList<Cliente>> ListarClientesDiaSiguienteAsync(
        int vendedoresId,
        int diaSemana,
        CancellationToken cancellationToken);
}
