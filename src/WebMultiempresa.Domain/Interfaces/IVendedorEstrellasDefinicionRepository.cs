using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorEstrellasDefinicionRepository
{
    Task<IReadOnlyList<VendedorEstrellasDefinicion>> ListarActivasAsync(CancellationToken cancellationToken);
    Task<VendedorEstrellasDefinicion?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExisteNumeroAsync(byte numero, int empresaId, int? excludeId, CancellationToken cancellationToken);
    Task<byte> ObtenerSiguienteNumeroAsync(int empresaId, CancellationToken cancellationToken);
    Task AgregarAsync(VendedorEstrellasDefinicion definicion, CancellationToken cancellationToken);
    Task ActualizarAsync(VendedorEstrellasDefinicion definicion, CancellationToken cancellationToken);
}
