using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Enums;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class BajaVendedorCommandHandler
{
    private readonly IVendedorRepository _repository;
    private readonly IActorEstadoLogRepository _logRepository;
    private readonly ITipoActorRepository _tipoActorRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public BajaVendedorCommandHandler(
        IVendedorRepository repository,
        IActorEstadoLogRepository logRepository,
        ITipoActorRepository tipoActorRepository,
        ICurrentEmpresaContext empresaContext)
    {
        _repository          = repository;
        _logRepository       = logRepository;
        _tipoActorRepository = tipoActorRepository;
        _empresaContext      = empresaContext;
    }

    public async Task HandleAsync(int vendedoresId, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        Vendedor vendedor = await _repository.ObtenerPorIdAsync(vendedoresId, cancellationToken)
            ?? throw new KeyNotFoundException($"Vendedor {vendedoresId} no encontrado.");

        vendedor.DarDeBaja();
        await _repository.ActualizarAsync(vendedor, cancellationToken);

        TipoActor tipoActor = await _tipoActorRepository.ObtenerPorCodigoAsync("VENDEDOR", cancellationToken)
            ?? throw new InvalidOperationException("TipoActor 'VENDEDOR' no encontrado. Verificar seed.");

        ActorEstadoLog log = ActorEstadoLog.Crear(
            tipoActor.TiposActoresID, vendedoresId, empresaId, TipoEventoActor.Baja);

        await _logRepository.RegistrarAsync(log, cancellationToken);
    }
}
