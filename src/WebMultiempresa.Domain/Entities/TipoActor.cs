namespace WebMultiempresa.Domain.Entities;

public sealed class TipoActor
{
    public int TiposActoresID { get; private set; }
    public int AplicacionesID { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Codigo { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private TipoActor() { }

    public static TipoActor Crear(int aplicacionesId, string nombre, string codigo) =>
        new() { AplicacionesID = aplicacionesId, Nombre = nombre.Trim(), Codigo = codigo.Trim().ToUpperInvariant(), Baja = false };
}
