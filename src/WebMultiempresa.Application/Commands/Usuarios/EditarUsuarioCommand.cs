namespace WebMultiempresa.Application.Commands.Usuarios;

public sealed class EditarUsuarioCommand
{
    public int UsuariosID { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public byte Rol { get; init; }
    public int? EmpresaID { get; init; }
    public string? NuevoPassword { get; init; }
}
