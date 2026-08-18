using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Productos;

public sealed class EditarProductoCommandHandler
{
    private readonly IProductoRepository _productoRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public EditarProductoCommandHandler(
        IProductoRepository productoRepository,
        ICurrentEmpresaContext empresaContext)
    {
        _productoRepository = productoRepository;
        _empresaContext = empresaContext;
    }

    public async Task HandleAsync(EditarProductoCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        Producto producto = await _productoRepository.ObtenerPorIdAsync(command.ProductosID, cancellationToken)
            ?? throw new KeyNotFoundException($"Producto {command.ProductosID} no encontrado.");

        bool codigoOcupado = await _productoRepository.ExisteCodigoAsync(
            empresaId, command.Codigo, excludeProductosID: command.ProductosID, cancellationToken);

        if (codigoOcupado)
            throw new ProductoCodigoExistenteException(command.Codigo);

        producto.Actualizar(
            command.Codigo,
            command.Nombre,
            command.ProductosIDPadre,
            command.MarcasProductosID,
            command.FamiliaProductosID,
            command.CantidadMultiplo,
            command.UnidadesPorBulto,
            command.StockPropio,
            command.ImpuestosID,
            command.UrlImagenWeb);
        await _productoRepository.ActualizarAsync(producto, cancellationToken);
    }
}
