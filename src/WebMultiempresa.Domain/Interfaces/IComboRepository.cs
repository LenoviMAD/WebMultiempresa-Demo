using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IComboRepository
{
    Task<bool> ExisteCodigoAsync(int empresaId, string codigo, int? excludeCombosID, CancellationToken cancellationToken);
    Task<IReadOnlyList<Combo>> ListarActivosAsync(CancellationToken cancellationToken);
    Task<Combo?> ObtenerPorIdAsync(int combosId, CancellationToken cancellationToken);
    Task<int> CrearAsync(Combo combo, CancellationToken cancellationToken);
    Task ActualizarAsync(Combo combo, CancellationToken cancellationToken);
    Task BajaLogicaAsync(int combosId, CancellationToken cancellationToken);
    Task ActualizarRelacionesAsync(int combosId, IReadOnlyList<int> vendedoresIds, IReadOnlyList<int> listasPreciosIds, IReadOnlyList<int> sucursalesIds, CancellationToken cancellationToken);
    Task ActualizarArrastreAsync(int combosId, IReadOnlyList<int> marcasProductosIds, IReadOnlyList<int> familiaProductosIds, CancellationToken cancellationToken);
    Task AgregarLogAsync(ComboLog log, CancellationToken cancellationToken);
    Task AgregarVigenciasAsync(DateTime fechaInicio, DateTime fechaFin, IReadOnlyList<int> combosIds, CancellationToken cancellationToken);
}
