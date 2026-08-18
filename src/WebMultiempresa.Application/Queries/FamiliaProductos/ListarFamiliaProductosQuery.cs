using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.FamiliaProductos;

public sealed class ListarFamiliaProductosQuery
{
    private readonly IFamiliaProductoRepository _repository;

    public ListarFamiliaProductosQuery(IFamiliaProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<FamiliaProductoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Entities.FamiliaProducto> items = await _repository.ListarActivasAsync(cancellationToken);
        return items.Select(m => new FamiliaProductoDto(m.FamiliaProductosID, m.Nombre)).ToList();
    }
}
