namespace WebMultiempresa.Application.Commands.Productos;

public sealed class EditarProductoCommand
{
    public int ProductosID { get; init; }
    public string Codigo { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public int? ProductosIDPadre { get; init; }
    public int? MarcasProductosID { get; init; }
    public int? FamiliaProductosID { get; init; }
    public int CantidadMultiplo { get; init; } = 1;
    public int UnidadesPorBulto { get; init; } = 1;
    public bool StockPropio { get; init; } = true;
    public int? ImpuestosID { get; init; }
    public string? UrlImagenWeb { get; init; }
}
