namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class GuardarEstrellasDefinicionCommand
{
    /// <summary>null = crear nueva; non-null = editar existente.</summary>
    public int? DefinicionId { get; init; }
    public int? CoeficienteId { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public decimal? ObjetivoMensual { get; init; }
    public decimal CoeficienteComision { get; init; } = 1m;
}
