using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Clientes;

public sealed class ListarClientesQuery
{
    private readonly IClienteRepository _repository;

    public ListarClientesQuery(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ClienteListadoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Cliente> clientes = await _repository.ListarActivosAsync(cancellationToken);
        return clientes
            .Select(c => new ClienteListadoDto(c.ClientesID, c.Codigo, c.Nombre))
            .ToList();
    }
}
