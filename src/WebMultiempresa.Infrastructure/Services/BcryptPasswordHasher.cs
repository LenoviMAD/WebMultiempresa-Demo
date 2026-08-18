using WebMultiempresa.Application.Ports;

namespace WebMultiempresa.Infrastructure.Services;

/// <summary>
/// Adaptador de BCrypt que implementa el port IPasswordHasher.
/// BCrypt vive únicamente en Infrastructure; Application solo conoce la abstracción.
/// </summary>
public sealed class BcryptPasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Verifica si el password plano coincide con el hash almacenado.
    /// </summary>
    public bool Verificar(string passwordPlano, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(passwordPlano, hash);
    }

    /// <summary>
    /// Genera un hash BCrypt a partir de un password en texto plano.
    /// </summary>
    public string Hashear(string passwordPlano)
    {
        return BCrypt.Net.BCrypt.HashPassword(passwordPlano);
    }
}
