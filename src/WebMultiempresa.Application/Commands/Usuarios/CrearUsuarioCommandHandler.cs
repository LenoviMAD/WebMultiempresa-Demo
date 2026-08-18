using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Usuarios;

public sealed class CrearUsuarioCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CrearUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<int> HandleAsync(CrearUsuarioCommand command, CancellationToken cancellationToken)
    {
        bool emailOcupado = await _usuarioRepository.ExisteEmailAsync(
            command.Email, excludeUsuariosID: null, cancellationToken);

        if (emailOcupado)
            throw new UsuarioEmailExistenteException(command.Email);

        string passwordHash = _passwordHasher.Hashear(command.Password);

        Usuario usuario = Usuario.Crear(
            empresaId: command.EmpresaID,
            email: command.Email,
            passwordHash: passwordHash,
            nombre: command.Nombre,
            rol: command.Rol);

        return await _usuarioRepository.CrearAsync(usuario, cancellationToken);
    }
}
