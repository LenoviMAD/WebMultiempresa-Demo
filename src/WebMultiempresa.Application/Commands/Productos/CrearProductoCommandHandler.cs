using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Exceptions;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Productos;

public sealed class CrearProductoCommandHandler
{
    private readonly IProductoRepository _productoRepository;
    private readonly ICurrentEmpresaContext _empresaContext;

    public CrearProductoCommandHandler(
        IProductoRepository productoRepository,
        ICurrentEmpresaContext empresaContext)
    {
        _productoRepository = productoRepository;
        _empresaContext = empresaContext;
    }

    public async Task<int> HandleAsync(CrearProductoCommand command, CancellationToken cancellationToken)
    {
        int empresaId = _empresaContext.EmpresaID
            ?? throw new InvalidOperationException("No hay empresa activa en el contexto.");

        bool codigoOcupado = await _productoRepository.ExisteCodigoAsync(
            empresaId, command.Codigo, excludeProductosID: null, cancellationToken);

        if (codigoOcupado)
            throw new ProductoCodigoExistenteException(command.Codigo);

        Producto producto = Producto.Crear(empresaId, command.Codigo, command.Nombre);
        return await _productoRepository.CrearAsync(producto, cancellationToken);
    }
}
