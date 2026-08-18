using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarEstrellasCoeficientesQuery
{
    private readonly IVendedorEstrellasCoeficienteRepository _repository;

    public ListarEstrellasCoeficientesQuery(IVendedorEstrellasCoeficienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<VendedorEstrellasCoeficienteDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<VendedorEstrellasCoeficiente> items = await _repository.ListarActivosAsync(cancellationToken);
        return items.Select(c => new VendedorEstrellasCoeficienteDto(
            c.VendedorEstrellasCoeficientesID,
            c.CantidadEstrellas,
            c.CoeficienteComision)).ToList();
    }
}
