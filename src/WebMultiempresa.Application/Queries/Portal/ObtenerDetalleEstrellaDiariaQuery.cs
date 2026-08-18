using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Portal;

public sealed class ObtenerDetalleEstrellaDiariaQuery
{
    private readonly IVendedorEstrellaDiariaRepository _estrellaDiariaRepo;

    public ObtenerDetalleEstrellaDiariaQuery(IVendedorEstrellaDiariaRepository estrellaDiariaRepo)
    {
        _estrellaDiariaRepo = estrellaDiariaRepo;
    }

    public async Task<IReadOnlyList<PortalEstrellaDiariaDto>> HandleAsync(
        int vendedoresId,
        int vendedorEstrellasDefinicionesId,
        DateTime fechaDesde,
        DateTime fechaHasta,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<VendedorEstrellaDiaria> datos =
            await _estrellaDiariaRepo.ListarPorVendedorYPeriodoAsync(
                vendedoresId, fechaDesde, fechaHasta, cancellationToken);

        return datos
            .Where(e => e.VendedorEstrellasDefinicionesID == vendedorEstrellasDefinicionesId)
            .OrderByDescending(e => e.Fecha)
            .Select(e => new PortalEstrellaDiariaDto(e.Fecha, Math.Round(e.Valor, 2), e.EstaEncendida))
            .ToList();
    }
}
