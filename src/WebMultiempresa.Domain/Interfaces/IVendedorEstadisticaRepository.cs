using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorEstadisticaRepository
{
    Task<VendedorEstadistica?> ObtenerMasRecienteAsync(
        int vendedoresId,
        CancellationToken cancellationToken);
}
