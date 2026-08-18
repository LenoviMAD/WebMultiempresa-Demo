using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class BajaEstrellasDefinicionCommandHandler
{
    private readonly IVendedorEstrellasDefinicionRepository _defRepo;
    private readonly IVendedorEstrellasCoeficienteRepository _coefRepo;
    private readonly ICurrentEmpresaContext _empresaContext;

    public BajaEstrellasDefinicionCommandHandler(
        IVendedorEstrellasDefinicionRepository defRepo,
        IVendedorEstrellasCoeficienteRepository coefRepo,
        ICurrentEmpresaContext empresaContext)
    {
        _defRepo        = defRepo;
        _coefRepo       = coefRepo;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(BajaEstrellasDefinicionCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        VendedorEstrellasDefinicion definicion = await _defRepo.ObtenerPorIdAsync(
            command.VendedorEstrellasDefinicionesID, cancellationToken)
            ?? throw new KeyNotFoundException($"Definición {command.VendedorEstrellasDefinicionesID} no encontrada.");

        definicion.DarDeBaja();
        await _defRepo.ActualizarAsync(definicion, cancellationToken);

        // Dar de baja también el coeficiente emparejado
        VendedorEstrellasCoeficiente? coef = await _coefRepo.ObtenerPorCantidadAsync(
            definicion.NumeroEstrella, empresaId, cancellationToken);

        if (coef is not null)
        {
            coef.DarDeBaja();
            await _coefRepo.ActualizarAsync(coef, cancellationToken);
        }
    }
}
