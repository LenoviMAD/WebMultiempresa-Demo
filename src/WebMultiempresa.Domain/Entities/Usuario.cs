namespace WebMultiempresa.Domain.Entities;

public sealed class Usuario
{
    public int UsuariosID { get; private set; }
    public int? EmpresaID { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public byte Rol { get; private set; }
    public bool Baja { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    public Empresa? Empresa { get; private set; }

    private Usuario() { }

    public static Usuario Crear(int? empresaId, string email, string passwordHash, string nombre, byte rol)
    {
        return new Usuario
        {
            EmpresaID = empresaId,
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHash,
            Nombre = nombre.Trim(),
            Rol = rol,
            FechaCreacion = DateTime.UtcNow
        };
    }

    public void Actualizar(string nombre, byte rol, int? empresaId)
    {
        Nombre = nombre.Trim();
        Rol = rol;
        EmpresaID = empresaId;
    }

    public void CambiarPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
}
