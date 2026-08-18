using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.Domain.Interfaces;

public interface IVendedorEstrellaDiariaRepository
{
    Task<IReadOnlyList<VendedorEstrellaDiaria>> ListarPorVendedorYPeriodoAsync(
        int vendedoresId,
        DateTime fechaDesde,
        DateTime fechaHasta,
        CancellationToken cancellationToken);
}
