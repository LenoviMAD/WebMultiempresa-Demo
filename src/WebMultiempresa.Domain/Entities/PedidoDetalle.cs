namespace WebMultiempresa.Domain.Entities;

public sealed class PedidoDetalle
{
    public int PedidosDetalleID { get; private set; }
    public int PedidosID { get; private set; }
    public int ProductosID { get; private set; }
    public decimal Cantidad { get; private set; }
    public decimal Precio { get; private set; }
    public decimal Descuento1 { get; private set; }
    public decimal Descuento2 { get; private set; }
    public bool EsCombo { get; private set; }
    public int? CombosID { get; private set; }
    public byte? TipoComboItem { get; private set; }
    public int ListasPreciosID { get; private set; }
    public decimal? UnidadesPorBulto { get; private set; }
    public int? GrupoDinamico { get; private set; }

    private PedidoDetalle() { }

    public static PedidoDetalle Crear(
        int pedidosId,
        int productosId,
        decimal cantidad,
        decimal precio,
        int listasPreciosId,
        decimal descuento1 = 0m,
        decimal descuento2 = 0m,
        bool esCombo = false,
        int? combosId = null,
        byte? tipoComboItem = null,
        decimal? unidadesPorBulto = null,
        int? grupoDinamico = null) =>
        new()
        {
            PedidosID       = pedidosId,
            ProductosID     = productosId,
            Cantidad        = cantidad,
            Precio          = precio,
            ListasPreciosID = listasPreciosId,
            Descuento1      = descuento1,
            Descuento2      = descuento2,
            EsCombo         = esCombo,
            CombosID        = combosId,
            TipoComboItem   = tipoComboItem,
            UnidadesPorBulto = unidadesPorBulto,
            GrupoDinamico   = grupoDinamico
        };
}
