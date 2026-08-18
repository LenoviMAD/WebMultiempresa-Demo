using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Sucursales;

public sealed class ListarSucursalesQuery
{
    private readonly ISucursalRepository _repository;

    public ListarSucursalesQuery(ISucursalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<SucursalDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Sucursal> sucursales = await _repository.ListarActivasAsync(cancellationToken);
        return sucursales.Select(s => new SucursalDto(s.SucursalesID, s.Nombre)).ToList();
    }
}
