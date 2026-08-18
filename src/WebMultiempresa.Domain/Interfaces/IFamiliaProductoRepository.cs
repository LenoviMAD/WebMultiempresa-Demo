using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IFamiliaProductoRepository
{
    Task<IReadOnlyList<FamiliaProducto>> ListarActivasAsync(CancellationToken cancellationToken);
}
