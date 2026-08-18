using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class GuardarEstrellasCoeficienteCommandHandler
{
    private readonly IVendedorEstrellasCoeficienteRepository _repository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public GuardarEstrellasCoeficienteCommandHandler(
        IVendedorEstrellasCoeficienteRepository repository,
        ICurrentEmpresaContext empresaContext)
    {
        _repository     = repository;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(GuardarEstrellasCoeficienteCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        if (command.VendedorEstrellasCoeficientesID is null)
        {
            VendedorEstrellasCoeficiente? existente = await _repository.ObtenerPorCantidadAsync(
                command.CantidadEstrellas, empresaId, cancellationToken);

            if (existente is not null)
                throw new InvalidOperationException(
                    $"Ya existe un coeficiente para {command.CantidadEstrellas} estrella(s).");

            VendedorEstrellasCoeficiente nuevo = VendedorEstrellasCoeficiente.Crear(
                empresaId, command.CantidadEstrellas, command.CoeficienteComision);

            await _repository.AgregarAsync(nuevo, cancellationToken);
        }
        else
        {
            VendedorEstrellasCoeficiente existente = await _repository.ObtenerPorIdAsync(
                command.VendedorEstrellasCoeficientesID.Value, cancellationToken)
                ?? throw new KeyNotFoundException($"Coeficiente {command.VendedorEstrellasCoeficientesID} no encontrado.");

            existente.ActualizarCoeficiente(command.CoeficienteComision);
            await _repository.ActualizarAsync(existente, cancellationToken);
        }
    }
}
