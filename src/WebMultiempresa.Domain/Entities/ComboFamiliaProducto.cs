namespace WebMultiempresa.Domain.Entities;

public sealed class ComboFamiliaProducto
{
    public int ComboFamiliaProductosID { get; private set; }
    public int CombosID { get; private set; }
    public int FamiliaProductosID { get; private set; }

    public FamiliaProducto? FamiliaProducto { get; private set; }

    private ComboFamiliaProducto() { }

    public static ComboFamiliaProducto Crear(int combosId, int familiaProductosId) =>
        new() { CombosID = combosId, FamiliaProductosID = familiaProductosId };
}
