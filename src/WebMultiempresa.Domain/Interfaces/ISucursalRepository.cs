using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface ISucursalRepository
{
    Task<IReadOnlyList<Sucursal>> ListarActivasAsync(CancellationToken cancellationToken);
}
