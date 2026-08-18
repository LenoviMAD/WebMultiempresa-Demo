namespace WebMultiempresa.Application.Ports;

/// <summary>
/// Puerto para verificación y generación de hashes de contraseña.
/// La implementación concreta (BCrypt) vive en Infrastructure.
/// </summary>
public interface IPasswordHasher
{
    bool Verificar(string passwordPlano, string hash);
    string Hashear(string passwordPlano);
}
