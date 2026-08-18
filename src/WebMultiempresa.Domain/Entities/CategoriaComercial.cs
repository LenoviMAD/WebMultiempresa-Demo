namespace WebMultiempresa.Domain.Entities;

public sealed class CategoriaComercial
{
    public int CategoriasComercialesID { get; private set; }
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private CategoriaComercial() { }

    public static CategoriaComercial Crear(int empresaId, string nombre) =>
        new()
        {
            EmpresaID = empresaId,
            Nombre    = nombre.Trim(),
            Baja      = false
        };

    public void Actualizar(string nombre)
    {
        Nombre = nombre.Trim();
    }

    public void DarDeBaja() => Baja = true;
}
