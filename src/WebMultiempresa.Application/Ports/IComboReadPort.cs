using WebMultiempresa.Application.DTOs;

namespace WebMultiempresa.Application.Ports;

public interface IComboReadPort
{
    Task<IReadOnlyList<ComboListadoDto>> ListarActivosAsync(CancellationToken cancellationToken);
}
