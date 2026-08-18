using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Portal;

public sealed class ObtenerResumenPortalVendedorQuery
{
    private readonly IVendedorRepository                    _vendedorRepo;
    private readonly IVendedorEstrellasDefinicionRepository _definicionRepo;
    private readonly IVendedorEstrellaDiariaRepository      _estrellaDiariaRepo;
    private readonly IVendedorEstadisticaRepository         _estadisticaRepo;

    public ObtenerResumenPortalVendedorQuery(
        IVendedorRepository                    vendedorRepo,
        IVendedorEstrellasDefinicionRepository definicionRepo,
        IVendedorEstrellaDiariaRepository      estrellaDiariaRepo,
        IVendedorEstadisticaRepository         estadisticaRepo)
    {
        _vendedorRepo       = vendedorRepo;
        _definicionRepo     = definicionRepo;
        _estrellaDiariaRepo = estrellaDiariaRepo;
        _estadisticaRepo    = estadisticaRepo;
    }

    public async Task<PortalVendedorResumenDto?> HandleAsync(
        int vendedoresId,
        DateTime fechaDesde,
        DateTime fechaHasta,
        CancellationToken cancellationToken)
    {
        Vendedor? vendedor = await _vendedorRepo.ObtenerPorIdAsync(vendedoresId, cancellationToken);
        if (vendedor is null) return null;

        IReadOnlyList<VendedorEstrellasDefinicion> definiciones =
            await _definicionRepo.ListarActivasAsync(cancellationToken);

        IReadOnlyList<VendedorEstrellaDiaria> diasEstrellas =
            await _estrellaDiariaRepo.ListarPorVendedorYPeriodoAsync(
                vendedoresId, fechaDesde, fechaHasta, cancellationToken);

        VendedorEstadistica? estadistica =
            await _estadisticaRepo.ObtenerMasRecienteAsync(vendedoresId, cancellationToken);

        DateTime fechaMasReciente = diasEstrellas.Any()
            ? diasEstrellas.Max(e => e.Fecha)
            : DateTime.MinValue;

        IReadOnlyList<PortalEstrellaResumenDto> estrellas = definiciones
            .OrderBy(d => d.NumeroEstrella)
            .Select(def =>
            {
                IEnumerable<VendedorEstrellaDiaria> diasDef = diasEstrellas
                    .Where(e => e.VendedorEstrellasDefinicionesID == def.VendedorEstrellasDefinicionesID);

                decimal promedio = diasDef.Any() ? diasDef.Average(e => e.Valor) : 0m;

                bool encendidaHoy = diasDef
                    .FirstOrDefault(e => e.Fecha == fechaMasReciente)
                    ?.EstaEncendida ?? false;

                return new PortalEstrellaResumenDto(
                    def.VendedorEstrellasDefinicionesID,
                    def.NumeroEstrella,
                    def.Nombre,
                    def.ObjetivoMensual,
                    Math.Round(promedio, 2),
                    encendidaHoy);
            })
            .ToList();

        decimal coeficiente        = estadistica?.CoeficienteComision ?? 0m;
        bool    coeficientePositivo = coeficiente >= 1m;
        decimal coeficienteDisplay  = coeficientePositivo
            ? (int)((coeficiente * 100) % 100)
            : 100 - (int)((coeficiente - (int)coeficiente) * 100);

        return new PortalVendedorResumenDto(
            vendedor.Nombre,
            coeficienteDisplay,
            coeficientePositivo,
            estrellas);
    }
}
