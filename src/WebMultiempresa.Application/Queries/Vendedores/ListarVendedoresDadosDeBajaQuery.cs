using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarVendedoresDadosDeBajaQuery
{
    private readonly IVendedorRepository _repository;

    public ListarVendedoresDadosDeBajaQuery(IVendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<VendedorListadoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Vendedor> vendedores = await _repository.ListarDadosDeBajaAsync(cancellationToken);
        return vendedores.Select(v => new VendedorListadoDto(
            v.VendedoresID,
            v.Codigo,
            v.Nombre,
            v.WhatsApp,
            v.CategoriasComercialesID,
            v.Baja)).ToList();
    }
}
