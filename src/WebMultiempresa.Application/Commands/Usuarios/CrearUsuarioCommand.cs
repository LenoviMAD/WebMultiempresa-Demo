namespace WebMultiempresa.Application.Commands.Usuarios;

public sealed class CrearUsuarioCommand
{
    public string Email { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public byte Rol { get; init; }
    public int? EmpresaID { get; init; }
}
