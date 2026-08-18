namespace WebMultiempresa.Domain.Entities;

public sealed class ProductoCategoriaComercial
{
    public int ProductoCategoriasComercialID { get; private set; }
    public int ProductosID { get; private set; }
    public int CategoriasComercialesID { get; private set; }
    public int EmpresaID { get; private set; }

    private ProductoCategoriaComercial() { }

    public static ProductoCategoriaComercial Crear(int productosId, int categoriasComercialesId, int empresaId) =>
        new()
        {
            ProductosID              = productosId,
            CategoriasComercialesID  = categoriasComercialesId,
            EmpresaID                = empresaId
        };
}
