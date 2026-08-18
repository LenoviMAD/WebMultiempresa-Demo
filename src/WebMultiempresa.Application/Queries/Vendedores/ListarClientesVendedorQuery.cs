using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarClientesVendedorQuery
{
    private readonly IClienteVendedorRepository _clienteVendedorRepository;
    private readonly IClienteRepository _clienteRepository;

    public ListarClientesVendedorQuery(
        IClienteVendedorRepository clienteVendedorRepository,
        IClienteRepository clienteRepository)
    {
        _clienteVendedorRepository = clienteVendedorRepository;
        _clienteRepository         = clienteRepository;
    }

    public async Task<IReadOnlyList<ClienteAsignadoDto>> HandleAsync(
        int vendedoresId,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<ClienteVendedor> asignaciones =
            await _clienteVendedorRepository.ListarActivosPorVendedorAsync(vendedoresId, cancellationToken);

        if (asignaciones.Count == 0)
            return [];

        IReadOnlyList<Cliente> clientes = await _clienteRepository.ListarActivosAsync(cancellationToken);
        Dictionary<int, Cliente> clientesPorId = clientes.ToDictionary(c => c.ClientesID);

        List<ClienteAsignadoDto> resultado = [];
        foreach (ClienteVendedor asignacion in asignaciones)
        {
            if (clientesPorId.TryGetValue(asignacion.ClientesID, out Cliente? cliente))
            {
                resultado.Add(new ClienteAsignadoDto(
                    asignacion.ClienteVendedoresID,
                    asignacion.ClientesID,
                    cliente.Codigo,
                    cliente.Nombre));
            }
        }

        return resultado.OrderBy(c => c.Nombre).ToList();
    }
}
