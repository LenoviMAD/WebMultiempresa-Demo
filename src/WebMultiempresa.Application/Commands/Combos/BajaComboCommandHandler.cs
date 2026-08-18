using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Combos;

public sealed class BajaComboCommandHandler
{
    private readonly IComboRepository _comboRepository;

    public BajaComboCommandHandler(IComboRepository comboRepository)
    {
        _comboRepository = comboRepository;
    }

    public async Task HandleAsync(int combosId, CancellationToken cancellationToken)
    {
        await _comboRepository.BajaLogicaAsync(combosId, cancellationToken);
    }
}
