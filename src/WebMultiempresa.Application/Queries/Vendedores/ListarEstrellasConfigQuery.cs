using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarEstrellasConfigQuery
{
    private readonly IVendedorEstrellasDefinicionRepository _defRepo;
    private readonly IVendedorEstrellasCoeficienteRepository _coefRepo;

    public ListarEstrellasConfigQuery(
        IVendedorEstrellasDefinicionRepository defRepo,
        IVendedorEstrellasCoeficienteRepository coefRepo)
    {
        _defRepo  = defRepo;
        _coefRepo = coefRepo;
    }

    public async Task<IReadOnlyList<VendedorEstrellasConfigDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<VendedorEstrellasDefinicion>   defs  = await _defRepo.ListarActivasAsync(cancellationToken);
        IReadOnlyList<VendedorEstrellasCoeficiente> coefs  = await _coefRepo.ListarActivosAsync(cancellationToken);

        Dictionary<byte, VendedorEstrellasCoeficiente> coefPorNumero =
            coefs.ToDictionary(c => c.CantidadEstrellas);

        List<VendedorEstrellasConfigDto> resultado = [];
        foreach (VendedorEstrellasDefinicion d in defs)
        {
            coefPorNumero.TryGetValue(d.NumeroEstrella, out VendedorEstrellasCoeficiente? coef);
            resultado.Add(new VendedorEstrellasConfigDto(
                d.VendedorEstrellasDefinicionesID,
                coef?.VendedorEstrellasCoeficientesID ?? 0,
                d.NumeroEstrella,
                d.Nombre,
                d.ObjetivoMensual,
                coef?.CoeficienteComision ?? 1m));
        }

        return resultado.OrderBy(x => x.NumeroEstrella).ToList();
    }
}
