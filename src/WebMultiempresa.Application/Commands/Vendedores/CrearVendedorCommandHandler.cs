using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Enums;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class CrearVendedorCommandHandler
{
    private readonly IVendedorRepository _repository;
    private readonly IActorEstadoLogRepository _logRepository;
    private readonly ITipoActorRepository _tipoActorRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public CrearVendedorCommandHandler(
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

    public async Task<int> HandleAsync(CrearVendedorCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        bool codigoOcupado = await _repository.ExisteCodigoAsync(
            command.Codigo, empresaId, excludeVendedoresId: null, cancellationToken);

        if (codigoOcupado)
            throw new VendedorCodigoExistenteException(command.Codigo);

        string? clave = string.IsNullOrWhiteSpace(command.Clave) ? null : command.Clave;

        Vendedor vendedor = Vendedor.Crear(
            empresaId,
            command.Codigo,
            command.Nombre,
            command.WhatsApp,
            clave,
            command.TiposDeVendedoresID,
            command.CategoriasComercialesID,
            command.DiaInicioRuta,
            command.VerTodasLasCategorias);

        await _repository.AgregarAsync(vendedor, cancellationToken);

        TipoActor tipoActor = await _tipoActorRepository.ObtenerPorCodigoAsync("VENDEDOR", cancellationToken)
            ?? throw new InvalidOperationException("TipoActor 'VENDEDOR' no encontrado. Verificar seed.");

        ActorEstadoLog log = ActorEstadoLog.Crear(
            tipoActor.TiposActoresID, vendedor.VendedoresID, empresaId, TipoEventoActor.Alta);

        await _logRepository.RegistrarAsync(log, cancellationToken);

        return vendedor.VendedoresID;
    }
}
