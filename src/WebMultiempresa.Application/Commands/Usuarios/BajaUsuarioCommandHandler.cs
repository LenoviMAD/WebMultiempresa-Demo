using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Usuarios;

public sealed class BajaUsuarioCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;

    public BajaUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task HandleAsync(int usuariosId, CancellationToken cancellationToken)
    {
        await _usuarioRepository.BajaLogicaAsync(usuariosId, cancellationToken);
    }
}
