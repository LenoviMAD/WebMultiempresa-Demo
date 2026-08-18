using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IEmpresaRepository
{
    Task<Empresa?> ObtenerPorIdAsync(int empresaId, CancellationToken cancellationToken);
    Task<IReadOnlyList<Empresa>> ListarActivasAsync(CancellationToken cancellationToken);
    Task<bool> ExisteNombreAsync(string nombre, int? excludeEmpresaID, CancellationToken cancellationToken);
    Task<int> CrearAsync(Empresa empresa, CancellationToken cancellationToken);
    Task ActualizarAsync(Empresa empresa, CancellationToken cancellationToken);
    Task BajaLogicaAsync(int empresaId, CancellationToken cancellationToken);
}
