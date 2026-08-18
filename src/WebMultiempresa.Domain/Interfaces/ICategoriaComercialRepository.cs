using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface ICategoriaComercialRepository
{
    Task<IReadOnlyList<CategoriaComercial>> ListarActivasAsync(CancellationToken cancellationToken);
}
