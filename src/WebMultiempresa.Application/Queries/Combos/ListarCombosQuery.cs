using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;

namespace WebMultiempresa.Application.Queries.Combos;

public sealed class ListarCombosQuery
{
    private readonly IComboReadPort _readPort;

    public ListarCombosQuery(IComboReadPort readPort)
    {
        _readPort = readPort;
    }

    public Task<IReadOnlyList<ComboListadoDto>> HandleAsync(CancellationToken cancellationToken)
        => _readPort.ListarActivosAsync(cancellationToken);
}
