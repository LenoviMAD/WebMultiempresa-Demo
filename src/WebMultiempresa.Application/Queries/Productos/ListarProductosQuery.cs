using Microsoft.Extensions.Options;
using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Settings;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Productos;

public sealed class ListarProductosQuery
{
    private readonly IProductoRepository _repository;
    private readonly ProductosSettings _settings;

    public ListarProductosQuery(IProductoRepository repository, IOptions<ProductosSettings> settings)
    {
        _repository = repository;
        _settings   = settings.Value;
    }

    public async Task<IReadOnlyList<ProductoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Entities.Producto> productos =
            await _repository.ListarActivosAsync(cancellationToken);

        IReadOnlyDictionary<int, DateTime> fechasPrecio =
            await _repository.ObtenerFechasUltimoPrecioAsync(cancellationToken);

        DateTime ahora = DateTime.UtcNow;

        return productos.Select(p =>
        {
            bool nuevoIngreso =
                (ahora - p.FechaAlta).TotalDays < _settings.DiasNuevoIngreso
                || (p.FechaUltimoReingreso.HasValue
                    && (ahora - p.FechaUltimoReingreso.Value).TotalDays < _settings.DiasNuevoIngreso);

            int diasPrecioMod = fechasPrecio.TryGetValue(p.ProductosID, out DateTime ultimaFechaPrecio)
                ? (int)(ahora - ultimaFechaPrecio).TotalDays
                : 9999;

            return new ProductoDto(
                ProductosID:            p.ProductosID,
                EmpresaID:              p.EmpresaID,
                Codigo:                 p.Codigo,
                Nombre:                 p.Nombre,
                PrecioCosto:            p.PrecioCosto,
                Baja:                   p.Baja,
                NuevoIngreso:           nuevoIngreso,
                DiasDePrecioModificado: diasPrecioMod
            );
        }).ToList();
    }
}
