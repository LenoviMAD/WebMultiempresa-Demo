namespace WebMultiempresa.Infrastructure.Persistence;

public sealed class UsuarioContexto
{
    public int? UsuariosID { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
}
