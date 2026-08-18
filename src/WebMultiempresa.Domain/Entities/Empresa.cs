namespace WebMultiempresa.Domain.Entities;

public sealed class Empresa
{
    public int EmpresaID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string KeyConexion { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private Empresa() { }

    public static Empresa Crear(string nombre, string keyConexion)
    {
        return new Empresa
        {
            Nombre = nombre.Trim(),
            KeyConexion = keyConexion.Trim()
        };
    }

    public void Actualizar(string nombre, string keyConexion)
    {
        Nombre = nombre.Trim();
        KeyConexion = keyConexion.Trim();
    }
}
