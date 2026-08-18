namespace WebMultiempresa.Application.DTOs;

/// <summary>
/// DTO de resultado del proceso de autenticación.
/// Contiene el token JWT y los datos del usuario autenticado.
/// </summary>
public sealed record LoginResultDto(
    string Token,
    UsuarioDto Usuario
);
