using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Enums;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class ReactivarVendedorCommandHandler
{
    private readonly IVendedorRepository _repository;
    private readonly IActorEstadoLogRepository _logRepository;
    private readonly ITipoActorRepository _tipoActorRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public ReactivarVendedorCommandHandler(
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

    public async Task HandleAsync(ReactivarVendedorCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        Vendedor vendedor = await _repository.ObtenerPorIdAsync(command.VendedoresID, cancellationToken)
            ?? throw new KeyNotFoundException($"Vendedor {command.VendedoresID} no encontrado.");

        vendedor.Reactivar();
        await _repository.ActualizarAsync(vendedor, cancellationToken);

        TipoActor tipoActor = await _tipoActorRepository.ObtenerPorCodigoAsync("VENDEDOR", cancellationToken)
            ?? throw new InvalidOperationException("TipoActor 'VENDEDOR' no encontrado. Verificar seed.");

        ActorEstadoLog log = ActorEstadoLog.Crear(
            tipoActor.TiposActoresID, command.VendedoresID, empresaId, TipoEventoActor.Alta);

        await _logRepository.RegistrarAsync(log, cancellationToken);
    }
}
