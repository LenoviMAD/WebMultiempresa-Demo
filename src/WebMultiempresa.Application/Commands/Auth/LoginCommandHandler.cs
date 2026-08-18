using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Auth;

/// <summary>
/// Caso de uso: autenticar un usuario con email y password.
/// Verifica la identidad contra el repositorio y genera un token JWT.
/// </summary>
public sealed class LoginCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUsuarioRepository usuarioRepository,
        IAuthService authService,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _authService = authService;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Ejecuta el login: valida credenciales y retorna el token JWT con los datos del usuario.
    /// Lanza <see cref="UnauthorizedAccessException"/> si las credenciales son inválidas o el usuario está dado de baja.
    /// </summary>
    public async Task<LoginResultDto> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        Usuario? usuario = await _usuarioRepository.ObtenerPorEmailAsync(
            command.Email, cancellationToken);

        if (usuario is null || usuario.Baja)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        bool passwordValida = _passwordHasher.Verificar(command.Password, usuario.PasswordHash);

        if (!passwordValida)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        string token = _authService.GenerarToken(
            usuario.UsuariosID, usuario.EmpresaID, usuario.Rol);

        UsuarioDto usuarioDto = new(
            usuario.UsuariosID,
            usuario.EmpresaID,
            usuario.Email,
            usuario.Nombre,
            usuario.Rol);

        return new LoginResultDto(token, usuarioDto);
    }
}
