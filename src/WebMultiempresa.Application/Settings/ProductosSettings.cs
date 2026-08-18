namespace WebMultiempresa.Application.Settings;

public sealed class ProductosSettings
{
    public const string SectionName = "Productos";

    public int DiasNuevoIngreso { get; init; } = 8;
    public int DiasPrecioModificado { get; init; } = 2;
}
