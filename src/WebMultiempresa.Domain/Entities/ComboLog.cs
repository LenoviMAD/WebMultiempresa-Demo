namespace WebMultiempresa.Domain.Entities;

public sealed class ComboLog
{
    public int ComboLogsID { get; private set; }
    public int CombosID { get; private set; }
    public int EmpresaID { get; private set; }
    public int UsuariosID { get; private set; }
    public string NombreUsuario { get; private set; } = string.Empty;
    public string PC { get; private set; } = string.Empty;
    public string Comentario { get; private set; } = string.Empty;
    public DateTime Fecha { get; private set; }

    private ComboLog() { }

    public static ComboLog Crear(int combosId, int empresaId, int usuariosId, string nombreUsuario, string comentario) =>
        new()
        {
            CombosID = combosId,
            EmpresaID = empresaId,
            UsuariosID = usuariosId,
            NombreUsuario = nombreUsuario,
            PC = "Web",
            Comentario = comentario,
            Fecha = DateTime.UtcNow
        };
}
