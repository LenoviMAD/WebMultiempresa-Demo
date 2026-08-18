using WebMultiempresa.Application.DTOs;

namespace WebMultiempresa.Application.Ports;

public interface IUsuarioReadPort
{
    Task<IReadOnlyList<UsuarioListadoDto>> ListarAsync(CancellationToken cancellationToken);
}
