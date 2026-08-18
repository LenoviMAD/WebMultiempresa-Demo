using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ContarActoresActivosPorMesQuery
{
    private readonly IActorEstadoLogRepository _logRepository;
    private readonly ITipoActorRepository _tipoActorRepository;

    public ContarActoresActivosPorMesQuery(
        IActorEstadoLogRepository logRepository,
        ITipoActorRepository tipoActorRepository)
    {
        _logRepository       = logRepository;
        _tipoActorRepository = tipoActorRepository;
    }

    public async Task<ActoresActivosMesDto> HandleAsync(
        int empresaId,
        string codigoTipoActor,
        DateTime inicioMes,
        DateTime finMes,
        CancellationToken cancellationToken)
    {
        Domain.Entities.TipoActor tipoActor =
            await _tipoActorRepository.ObtenerPorCodigoAsync(codigoTipoActor, cancellationToken)
            ?? throw new InvalidOperationException($"TipoActor '{codigoTipoActor}' no encontrado.");

        int cantidad = await _logRepository.ContarActivosPorMesAsync(
            tipoActor.TiposActoresID, empresaId, inicioMes, finMes, cancellationToken);

        return new ActoresActivosMesDto(empresaId, tipoActor.TiposActoresID, inicioMes, finMes, cantidad);
    }
}
