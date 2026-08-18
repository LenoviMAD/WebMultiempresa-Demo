namespace WebMultiempresa.Domain.Entities;

// Catálogo global — no tiene EmpresaID (DNI, CUIT, Pasaporte son universales)
public sealed class TipoDocumento
{
    public int TiposDocumentosID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;

    private TipoDocumento() { }

    public static TipoDocumento Crear(string nombre) =>
        new() { Nombre = nombre.Trim() };

    public void Actualizar(string nombre) => Nombre = nombre.Trim();
}
