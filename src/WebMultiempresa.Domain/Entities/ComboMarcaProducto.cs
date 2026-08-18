namespace WebMultiempresa.Domain.Entities;

public sealed class ComboMarcaProducto
{
    public int ComboMarcasProductosID { get; private set; }
    public int CombosID { get; private set; }
    public int MarcasProductosID { get; private set; }

    public MarcaProducto? MarcaProducto { get; private set; }

    private ComboMarcaProducto() { }

    public static ComboMarcaProducto Crear(int combosId, int marcasProductosId) =>
        new() { CombosID = combosId, MarcasProductosID = marcasProductosId };
}
