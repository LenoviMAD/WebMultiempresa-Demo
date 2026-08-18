using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;

namespace WebMultiempresa.Application.Queries.Usuarios;

public sealed class ListarUsuariosQuery
{
    private readonly IUsuarioReadPort _readPort;

    public ListarUsuariosQuery(IUsuarioReadPort readPort)
    {
        _readPort = readPort;
    }

    public Task<IReadOnlyList<UsuarioListadoDto>> HandleAsync(CancellationToken cancellationToken)
        => _readPort.ListarAsync(cancellationToken);
}
