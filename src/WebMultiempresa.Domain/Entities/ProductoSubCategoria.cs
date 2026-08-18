namespace WebMultiempresa.Domain.Entities;

public sealed class ProductoSubCategoria
{
    public int ProductoSubCategoriasID { get; private set; }
    public int EmpresaID { get; private set; }
    public int ProductosID { get; private set; }
    public int SubCategoriasProductosID { get; private set; }
    public bool Baja { get; private set; }

    private ProductoSubCategoria() { }

    public static ProductoSubCategoria Crear(int empresaId, int productosId, int subCategoriasProductosId) =>
        new()
        {
            EmpresaID               = empresaId,
            ProductosID             = productosId,
            SubCategoriasProductosID = subCategoriasProductosId,
            Baja                    = false
        };

    public void DarDeBaja() => Baja = true;
}
