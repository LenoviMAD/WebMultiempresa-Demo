namespace WebMultiempresa.Domain.Entities;

public sealed class Aplicacion
{
    public int AplicacionesID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string? Descripcion { get; private set; }
    public bool Baja { get; private set; }

    private Aplicacion() { }

    public static Aplicacion Crear(string nombre, string? descripcion = null) =>
        new() { Nombre = nombre.Trim(), Descripcion = descripcion?.Trim(), Baja = false };

    public void Actualizar(string nombre, string? descripcion)
    {
        Nombre = nombre.Trim();
        Descripcion = descripcion?.Trim();
    }
}
