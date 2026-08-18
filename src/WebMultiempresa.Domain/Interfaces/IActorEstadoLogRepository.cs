using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IActorEstadoLogRepository
{
    Task RegistrarAsync(ActorEstadoLog log, CancellationToken cancellationToken);
    Task<int> ContarActivosPorMesAsync(
        int tiposActoresId,
        int empresaId,
        DateTime inicioMes,
        DateTime finMes,
        CancellationToken cancellationToken);
}
