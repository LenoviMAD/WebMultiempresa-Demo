using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorListaPreciosRepository
{
    Task<IReadOnlyList<VendedorListaPrecios>> ListarActivasPorVendedorAsync(int vendedoresId, CancellationToken cancellationToken);
    Task<VendedorListaPrecios?> ObtenerPorIdAsync(int vendedorListasPreciosId, CancellationToken cancellationToken);
    Task<VendedorListaPrecios?> ObtenerDefaultActivaAsync(int vendedoresId, CancellationToken cancellationToken);
    Task<bool> ExisteAsignacionActivaAsync(int vendedoresId, int listaPreciosId, CancellationToken cancellationToken);
    Task AgregarAsync(VendedorListaPrecios lista, CancellationToken cancellationToken);
    Task ActualizarAsync(VendedorListaPrecios lista, CancellationToken cancellationToken);
}
