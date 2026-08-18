using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorRepository
{
    Task<IReadOnlyList<Vendedor>> ListarActivosAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Vendedor>> ListarDadosDeBajaAsync(CancellationToken cancellationToken);
    Task<Vendedor?> ObtenerPorIdAsync(int vendedoresId, CancellationToken cancellationToken);
    Task<bool> ExisteCodigoAsync(string codigo, int empresaId, int? excludeVendedoresId, CancellationToken cancellationToken);
    Task<Vendedor?> ObtenerPorCodigoAsync(string codigo, int empresaId, CancellationToken cancellationToken);
    Task AgregarAsync(Vendedor vendedor, CancellationToken cancellationToken);
    Task ActualizarAsync(Vendedor vendedor, CancellationToken cancellationToken);

    /// <summary>
    /// Devuelve VendedoresID → set de VendedorEstrellasDefinicionesID con EstaEncendida = true
    /// en la estadística más reciente de cada vendedor.
    /// </summary>
    Task<IReadOnlyDictionary<int, IReadOnlySet<int>>> ObtenerEstrellasEncendidasPorVendedorAsync(CancellationToken cancellationToken);
}
