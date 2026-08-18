namespace WebMultiempresa.Domain.Entities;

public sealed class CategoriaSubCategoriaRelacion
{
    public int CategoriaSubCategoriaRelacionID { get; private set; }
    public int EmpresaID { get; private set; }
    public int CategoriasProductosID { get; private set; }
    public int SubCategoriasProductosID { get; private set; }

    public CategoriaProducto? Categoria { get; private set; }
    public SubCategoriaProducto? SubCategoria { get; private set; }

    private CategoriaSubCategoriaRelacion() { }

    public static CategoriaSubCategoriaRelacion Crear(
        int empresaId,
        int categoriasProductosId,
        int subCategoriasProductosId)
    {
        return new CategoriaSubCategoriaRelacion
        {
            EmpresaID               = empresaId,
            CategoriasProductosID   = categoriasProductosId,
            SubCategoriasProductosID = subCategoriasProductosId
        };
    }
}
