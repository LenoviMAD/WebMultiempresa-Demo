using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class GuardarEstrellasDefinicionCommandHandler
{
    private readonly IVendedorEstrellasDefinicionRepository _defRepo;
    private readonly IVendedorEstrellasCoeficienteRepository _coefRepo;
    private readonly ICurrentEmpresaContext _empresaContext;

    public GuardarEstrellasDefinicionCommandHandler(
        IVendedorEstrellasDefinicionRepository defRepo,
        IVendedorEstrellasCoeficienteRepository coefRepo,
        ICurrentEmpresaContext empresaContext)
    {
        _defRepo        = defRepo;
        _coefRepo       = coefRepo;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(GuardarEstrellasDefinicionCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        if (command.DefinicionId is null)
        {
            // Crear: número auto-asignado, genera definición + coeficiente juntos
            byte numero = await _defRepo.ObtenerSiguienteNumeroAsync(empresaId, cancellationToken);

            VendedorEstrellasDefinicion nueva = VendedorEstrellasDefinicion.Crear(
                empresaId, numero, command.Nombre, command.ObjetivoMensual);

            await _defRepo.AgregarAsync(nueva, cancellationToken);

            // Si existe un coeficiente con ese número (p.ej. de un intento anterior interrumpido)
            // lo reactiva; si no existe, crea uno nuevo
            VendedorEstrellasCoeficiente? coefExistente =
                await _coefRepo.ObtenerPorCantidadAsync(numero, empresaId, cancellationToken);

            if (coefExistente is not null)
            {
                coefExistente.Reactivar();
                coefExistente.ActualizarCoeficiente(command.CoeficienteComision);
                await _coefRepo.ActualizarAsync(coefExistente, cancellationToken);
            }
            else
            {
                await _coefRepo.AgregarAsync(
                    VendedorEstrellasCoeficiente.Crear(empresaId, numero, command.CoeficienteComision),
                    cancellationToken);
            }
        }
        else
        {
            // Editar: actualiza definición y coeficiente existentes
            VendedorEstrellasDefinicion def = await _defRepo.ObtenerPorIdAsync(
                command.DefinicionId.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"Definición {command.DefinicionId} no encontrada.");

            def.Actualizar(command.Nombre, command.ObjetivoMensual);
            await _defRepo.ActualizarAsync(def, cancellationToken);

            if (command.CoeficienteId.HasValue && command.CoeficienteId.Value > 0)
            {
                VendedorEstrellasCoeficiente coef = await _coefRepo.ObtenerPorIdAsync(
                    command.CoeficienteId.Value, cancellationToken)
                    ?? throw new KeyNotFoundException($"Coeficiente {command.CoeficienteId} no encontrado.");

                coef.ActualizarCoeficiente(command.CoeficienteComision);
                await _coefRepo.ActualizarAsync(coef, cancellationToken);
            }
        }
    }
}
