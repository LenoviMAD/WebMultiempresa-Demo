namespace WebMultiempresa.Domain.Entities;

public sealed class CategoriaProducto
{
    public int CategoriasProductosID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string UrlImagen { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private readonly List<CategoriaSubCategoriaRelacion> _relaciones = new();
    public IReadOnlyCollection<CategoriaSubCategoriaRelacion> Relaciones => _relaciones.AsReadOnly();

    private CategoriaProducto() { }

    public static CategoriaProducto Crear(int empresaId, string nombre, string urlImagen = "") =>
        new()
        {
            EmpresaID = empresaId,
            Nombre    = nombre.Trim(),
            UrlImagen = urlImagen,
            Baja      = false
        };

    public void Actualizar(string nombre, string urlImagen)
    {
        Nombre    = nombre.Trim();
        UrlImagen = urlImagen;
    }

    public void DarDeBaja() => Baja = true;
    public void Restaurar() => Baja = false;
}
