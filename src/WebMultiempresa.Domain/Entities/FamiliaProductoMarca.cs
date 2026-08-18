namespace WebMultiempresa.Domain.Entities;

public sealed class FamiliaProductoMarca
{
    public int FamiliaProductoMarcasID { get; private set; }
    public int EmpresaID { get; private set; }
    public int FamiliaProductosID { get; private set; }
    public int MarcasProductosID { get; private set; }
    public bool Baja { get; private set; }

    private FamiliaProductoMarca() { }

    public static FamiliaProductoMarca Crear(int empresaId, int familiaProductosId, int marcasProductosId) =>
        new()
        {
            EmpresaID          = empresaId,
            FamiliaProductosID = familiaProductosId,
            MarcasProductosID  = marcasProductosId,
            Baja               = false
        };

    public void DarDeBaja() => Baja = true;
}
