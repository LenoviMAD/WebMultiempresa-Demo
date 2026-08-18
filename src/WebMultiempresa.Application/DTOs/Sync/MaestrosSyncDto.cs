namespace WebMultiempresa.Application.DTOs.Sync;

public sealed class MaestrosSyncDto
{
    public int EmpresaID { get; init; }

    public List<ImpuestoSyncItem>        Impuestos        { get; init; } = [];
    public List<MarcaProductoSyncItem>   MarcasProductos  { get; init; } = [];
    public List<FamiliaProductoSyncItem> FamiliasProductos { get; init; } = [];
    public List<CategoriaProductoSyncItem>    Categorias    { get; init; } = [];
    public List<SubCategoriaProductoSyncItem> SubCategorias { get; init; } = [];
    public List<CategoriaComercialSyncItem> CategoriasComerciales { get; init; } = [];
    public List<ListaPreciosSyncItem>    ListasPrecios     { get; init; } = [];
    public List<TipoDocumentoSyncItem>   TiposDocumentos   { get; init; } = [];
}

public sealed class ImpuestoSyncItem
{
    public int     ImpuestosID { get; init; }
    public string  Nombre      { get; init; } = string.Empty;
    public decimal Porcentaje  { get; init; }
}

public sealed class MarcaProductoSyncItem
{
    public int    MarcasProductosID { get; init; }
    public string Nombre            { get; init; } = string.Empty;
}

public sealed class FamiliaProductoSyncItem
{
    public int    FamiliaProductosID { get; init; }
    public string Nombre             { get; init; } = string.Empty;
}

public sealed class CategoriaProductoSyncItem
{
    public int    CategoriasProductosID { get; init; }
    public string Nombre                { get; init; } = string.Empty;
    public string UrlImagen             { get; init; } = string.Empty;
}

public sealed class SubCategoriaProductoSyncItem
{
    public int    SubCategoriasProductosID { get; init; }
    public string Nombre                   { get; init; } = string.Empty;
    public string UrlImagen                { get; init; } = string.Empty;
    public int    CategoriasProductosID    { get; init; }
}

public sealed class CategoriaComercialSyncItem
{
    public int    CategoriasComercialesID { get; init; }
    public string Nombre                  { get; init; } = string.Empty;
}

public sealed class ListaPreciosSyncItem
{
    public int     ListasPreciosID   { get; init; }
    public string  Nombre            { get; init; } = string.Empty;
    public decimal PorcentajeMarcup  { get; init; }
}

public sealed class TipoDocumentoSyncItem
{
    public int    TiposDocumentosID { get; init; }
    public string Nombre            { get; init; } = string.Empty;
}
