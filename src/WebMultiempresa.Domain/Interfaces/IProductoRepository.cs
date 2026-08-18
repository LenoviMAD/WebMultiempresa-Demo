using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IProductoRepository
{
    Task<IReadOnlyList<Producto>> ListarActivosAsync(CancellationToken cancellationToken);
    Task<Producto?> ObtenerPorIdAsync(int productosId, CancellationToken cancellationToken);
    Task<IReadOnlyList<(int ListasPreciosID, string NombreLista, decimal PrecioFinal)>> ObtenerPreciosAsync(int productosId, CancellationToken cancellationToken);
    Task<IReadOnlyDictionary<int, DateTime>> ObtenerFechasUltimoPrecioAsync(CancellationToken cancellationToken);
    Task<bool> ExisteCodigoAsync(int empresaId, string codigo, int? excludeProductosID, CancellationToken cancellationToken);
    Task<int> CrearAsync(Producto producto, CancellationToken cancellationToken);
    Task ActualizarAsync(Producto producto, CancellationToken cancellationToken);
    Task BajaLogicaAsync(int productosId, CancellationToken cancellationToken);
}
