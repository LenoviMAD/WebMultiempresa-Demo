using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Combos;

public sealed class AgregarVigenciaCombosCommandHandler
{
    private readonly IComboRepository _repository;

    public AgregarVigenciaCombosCommandHandler(IComboRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AgregarVigenciaCombosCommand command, CancellationToken cancellationToken)
    {
        if (command.CombosIDs.Count == 0)
            throw new InvalidOperationException("Debe seleccionar al menos un combo.");

        if (command.FechaFin < command.FechaInicio)
            throw new InvalidOperationException("La fecha de fin debe ser posterior a la fecha de inicio.");

        await _repository.AgregarVigenciasAsync(
            command.FechaInicio,
            command.FechaFin,
            command.CombosIDs,
            cancellationToken);
    }
}
