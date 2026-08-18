using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IListaPreciosRepository
{
    Task<IReadOnlyList<ListaPrecios>> ListarActivasAsync(CancellationToken cancellationToken);
}
