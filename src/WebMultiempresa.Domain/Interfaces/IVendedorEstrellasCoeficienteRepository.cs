using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorEstrellasCoeficienteRepository
{
    Task<IReadOnlyList<VendedorEstrellasCoeficiente>> ListarActivosAsync(CancellationToken cancellationToken);
    Task<VendedorEstrellasCoeficiente?> ObtenerPorCantidadAsync(byte cantidad, int empresaId, CancellationToken cancellationToken);
    Task<VendedorEstrellasCoeficiente?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken);
    Task AgregarAsync(VendedorEstrellasCoeficiente coeficiente, CancellationToken cancellationToken);
    Task ActualizarAsync(VendedorEstrellasCoeficiente coeficiente, CancellationToken cancellationToken);
}
