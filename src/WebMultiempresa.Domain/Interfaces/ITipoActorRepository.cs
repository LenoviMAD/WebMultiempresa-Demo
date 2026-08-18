using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface ITipoActorRepository
{
    Task<TipoActor?> ObtenerPorCodigoAsync(string codigo, CancellationToken cancellationToken);
}
