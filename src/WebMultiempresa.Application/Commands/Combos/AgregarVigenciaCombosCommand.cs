namespace WebMultiempresa.Application.Commands.Combos;

public sealed class AgregarVigenciaCombosCommand
{
    public DateTime FechaInicio { get; init; }
    public DateTime FechaFin { get; init; }
    public IReadOnlyList<int> CombosIDs { get; init; } = [];
}
