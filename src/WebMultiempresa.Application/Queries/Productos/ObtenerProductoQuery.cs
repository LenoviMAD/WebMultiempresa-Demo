using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Productos;

public sealed class ObtenerProductoQuery
{
    private readonly IProductoRepository _repository;

    public ObtenerProductoQuery(IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductoDto?> HandleAsync(int productosId, CancellationToken cancellationToken)
    {
        Domain.Entities.Producto? producto = await _repository.ObtenerPorIdAsync(productosId, cancellationToken);

        if (producto is null) return null;

        IReadOnlyList<(int ListasPreciosID, string NombreLista, decimal PrecioFinal)> precios =
            await _repository.ObtenerPreciosAsync(productosId, cancellationToken);

        return new ProductoDto(
            ProductosID: producto.ProductosID,
            EmpresaID: producto.EmpresaID,
            Codigo: producto.Codigo,
            Nombre: producto.Nombre,
            PrecioCosto: producto.PrecioCosto,
            Baja: producto.Baja,
            Precios: precios.Select(p => new ProductoPrecioDto(p.ListasPreciosID, p.NombreLista, p.PrecioFinal)).ToList());
    }
}
