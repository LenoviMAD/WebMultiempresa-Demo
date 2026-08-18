using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Productos;

public sealed class ObtenerProductosParaComboQuery
{
    private readonly IProductoRepository _repository;

    public ObtenerProductosParaComboQuery(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        System.Collections.Generic.IReadOnlyList<Domain.Entities.Producto> productos =
            await _repository.ListarActivosAsync(cancellationToken);

        return productos.Select(p => new ProductoDto(
            ProductosID: p.ProductosID,
            EmpresaID: p.EmpresaID,
            Codigo: p.Codigo,
            Nombre: p.Nombre,
            PrecioCosto: p.PrecioCosto,
            Baja: p.Baja
        )).ToList();
    }
}
