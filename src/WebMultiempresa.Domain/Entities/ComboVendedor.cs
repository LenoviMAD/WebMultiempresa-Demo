namespace WebMultiempresa.Domain.Entities;

public sealed class ComboVendedor
{
    public int ComboVendedorID { get; private set; }
    public int CombosID { get; private set; }
    public int VendedoresID { get; private set; }

    private ComboVendedor() { }

    public static ComboVendedor Crear(int combosId, int vendedoresId) =>
        new() { CombosID = combosId, VendedoresID = vendedoresId };
}
