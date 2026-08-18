namespace WebMultiempresa.Domain.Entities;

public sealed class UsuarioAppPermiso
{
    public int UsuarioAppPermisosID { get; private set; }
    public int UsuariosID { get; private set; }
    public int AplicacionesID { get; private set; }
    public string Permiso { get; private set; } = string.Empty;
    public bool Baja { get; private set; }

    private UsuarioAppPermiso() { }

    public static UsuarioAppPermiso Crear(int usuariosId, int aplicacionesId, string permiso) =>
        new() { UsuariosID = usuariosId, AplicacionesID = aplicacionesId, Permiso = permiso.Trim(), Baja = false };
}
