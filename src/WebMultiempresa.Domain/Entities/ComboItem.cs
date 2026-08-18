namespace WebMultiempresa.Domain.Entities;

public sealed class ComboItem
{
    public int ComboItemsID { get; private set; }
    public int CombosID { get; private set; }
    public int ProductosID { get; private set; }
    public decimal Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public byte Tipo { get; private set; }
    public decimal Descuento1 { get; private set; }
    public decimal Descuento2 { get; private set; }
    public int NroGrupoDinamico { get; private set; }

    public Producto? Producto { get; private set; }

    private ComboItem() { }

    public static ComboItem Crear(
        int combosId,
        int productosId,
        decimal cantidad,
        decimal precio,
        byte tipo,
        decimal descuento1 = 0m,
        decimal descuento2 = 0m,
        int nroGrupoDinamico = 0)
    {
        return new ComboItem
        {
            CombosID = combosId,
            ProductosID = productosId,
            Cantidad = cantidad,
            Precio = precio,
            Tipo = tipo,
            Descuento1 = descuento1,
            Descuento2 = descuento2,
            NroGrupoDinamico = nroGrupoDinamico
        };
    }
}
