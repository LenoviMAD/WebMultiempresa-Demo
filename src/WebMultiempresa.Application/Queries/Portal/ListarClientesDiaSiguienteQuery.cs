using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Portal;

public sealed class ListarClientesDiaSiguienteQuery
{
    private readonly IClienteVendedorRepository _clienteVendedorRepo;

    public ListarClientesDiaSiguienteQuery(IClienteVendedorRepository clienteVendedorRepo)
    {
        _clienteVendedorRepo = clienteVendedorRepo;
    }

    public async Task<IReadOnlyList<PortalClienteDiaSiguienteDto>> HandleAsync(
        int vendedoresId,
        CancellationToken cancellationToken)
    {
        DayOfWeek dow       = DateTime.Today.AddDays(1).DayOfWeek;
        int       diaSemana = (int)dow == 0 ? 6 : (int)dow - 1;

        IReadOnlyList<Cliente> clientes = await _clienteVendedorRepo.ListarClientesDiaSiguienteAsync(
            vendedoresId, diaSemana, cancellationToken);

        return clientes
            .Select(c => new PortalClienteDiaSiguienteDto(
                c.ClientesID,
                c.Codigo,
                c.Nombre,
                c.Telefono,
                c.Direccion))
            .ToList();
    }
}
