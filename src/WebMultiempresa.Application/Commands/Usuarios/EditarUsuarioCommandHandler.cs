using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Usuarios;

public sealed class EditarUsuarioCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;

    public EditarUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(EditarUsuarioCommand command, CancellationToken cancellationToken)
    {
        Usuario usuario = await _usuarioRepository.ObtenerPorIdAsync(command.UsuariosID, cancellationToken)
            ?? throw new KeyNotFoundException($"Usuario {command.UsuariosID} no encontrado.");

        usuario.Actualizar(command.Nombre, command.Rol, command.EmpresaID);

        if (!string.IsNullOrWhiteSpace(command.NuevoPassword))
            usuario.CambiarPassword(_passwordHasher.Hashear(command.NuevoPassword));

        await _usuarioRepository.ActualizarAsync(usuario, cancellationToken);
    }
}
