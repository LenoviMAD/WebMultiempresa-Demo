using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarEstrellasDefinicionesQuery
{
    private readonly IVendedorEstrellasDefinicionRepository _repository;

    public ListarEstrellasDefinicionesQuery(IVendedorEstrellasDefinicionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<VendedorEstrellasDefinicionDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<VendedorEstrellasDefinicion> items = await _repository.ListarActivasAsync(cancellationToken);
        return items.Select(d => new VendedorEstrellasDefinicionDto(
            d.VendedorEstrellasDefinicionesID,
            d.NumeroEstrella,
            d.Nombre,
            d.ObjetivoMensual)).ToList();
    }
}
