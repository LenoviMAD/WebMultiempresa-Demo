namespace WebMultiempresa.Domain.Entities;

public sealed class ComboSucursal
{
    public int ComboSucursalID { get; private set; }
    public int CombosID { get; private set; }
    public int SucursalesID { get; private set; }

    private ComboSucursal() { }

    public static ComboSucursal Crear(int combosId, int sucursalesId) =>
        new() { CombosID = combosId, SucursalesID = sucursalesId };
}
