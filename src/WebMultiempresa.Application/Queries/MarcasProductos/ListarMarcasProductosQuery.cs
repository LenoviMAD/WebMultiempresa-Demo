using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.MarcasProductos;

public sealed class ListarMarcasProductosQuery
{
    private readonly IMarcaProductoRepository _repository;

    public ListarMarcasProductosQuery(IMarcaProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<MarcaProductoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        System.Collections.Generic.IReadOnlyList<Domain.Entities.MarcaProducto> items =
            await _repository.ListarActivosAsync(cancellationToken);

        return items.Select(r => new MarcaProductoDto(r.MarcasProductosID, r.Nombre)).ToList();
    }
}
