using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Productos;

public sealed class BajaProductoCommandHandler
{
    private readonly IProductoRepository _productoRepository;

    public BajaProductoCommandHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task HandleAsync(int productosId, CancellationToken cancellationToken)
    {
        await _productoRepository.BajaLogicaAsync(productosId, cancellationToken);
    }
}
