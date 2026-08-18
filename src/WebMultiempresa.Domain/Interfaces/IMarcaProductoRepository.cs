using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IMarcaProductoRepository
{
    Task<IReadOnlyList<MarcaProducto>> ListarActivosAsync(CancellationToken cancellationToken);
}
