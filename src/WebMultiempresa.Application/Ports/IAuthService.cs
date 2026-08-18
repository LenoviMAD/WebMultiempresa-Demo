namespace WebMultiempresa.Application.Ports;

/// <summary>
/// Puerto para la generación y lectura de tokens JWT.
/// Implementado en Infrastructure mediante la librería de JWT configurada.
/// </summary>
public interface IAuthService
{
    /// <summary>Genera un token JWT con los claims del usuario.</summary>
    string GenerarToken(int usuarioId, int? empresaId, byte rol);

    /// <summary>Extrae el EmpresaID del token; retorna null si no está presente o el token es inválido.</summary>
    int? ObtenerEmpresaIdDelToken(string token);

    /// <summary>Genera un token JWT para un vendedor autenticado en el portal.</summary>
    string GenerarTokenVendedor(int vendedoresId, int empresaId, string nombreVendedor);
}
