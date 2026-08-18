namespace WebMultiempresa.Domain.Entities;

public sealed class ProductoPrecio
{
    public int ProductoPreciosID { get; private set; }
    public int ProductosID { get; private set; }
    public int ListasPreciosID { get; private set; }
    public int EmpresaID { get; private set; }
    public decimal PrecioFinal { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaActualizacion { get; private set; }

    private ProductoPrecio() { }

    public static ProductoPrecio Crear(int productosId, int listasPreciosId, int empresaId, decimal precioFinal) =>
        new()
        {
            ProductosID        = productosId,
            ListasPreciosID    = listasPreciosId,
            EmpresaID          = empresaId,
            PrecioFinal        = precioFinal,
            Baja               = false,
            FechaActualizacion = DateTime.UtcNow
        };

    public void ActualizarPrecio(decimal precioFinal)
    {
        PrecioFinal        = precioFinal;
        FechaActualizacion = DateTime.UtcNow;
    }
}
