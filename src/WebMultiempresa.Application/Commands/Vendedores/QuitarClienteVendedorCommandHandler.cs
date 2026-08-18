using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class QuitarClienteVendedorCommandHandler
{
    private readonly IClienteVendedorRepository _repository;

    public QuitarClienteVendedorCommandHandler(IClienteVendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(QuitarClienteVendedorCommand command, CancellationToken cancellationToken)
    {
        ClienteVendedor asignacion = await _repository.ObtenerAsync(
            command.ClientesID, command.VendedoresID, cancellationToken)
            ?? throw new KeyNotFoundException(
                $"Asignación cliente {command.ClientesID} / vendedor {command.VendedoresID} no encontrada.");

        asignacion.DarDeBaja();
        await _repository.ActualizarAsync(asignacion, cancellationToken);
    }
}
