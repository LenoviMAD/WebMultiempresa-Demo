using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarVendedoresQuery
{
    private readonly IVendedorRepository _repository;

    public ListarVendedoresQuery(IVendedorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<VendedorListadoDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Vendedor> vendedores = await _repository.ListarActivosAsync(cancellationToken);
        IReadOnlyDictionary<int, IReadOnlySet<int>> estrellasEncendidas =
            await _repository.ObtenerEstrellasEncendidasPorVendedorAsync(cancellationToken);

        return vendedores
            .OrderBy(v => v.CategoriasComercialesID ?? int.MaxValue)
            .ThenBy(v => v.Nombre)
            .Select(v => new VendedorListadoDto(
                v.VendedoresID,
                v.Codigo,
                v.Nombre,
                v.WhatsApp,
                v.CategoriasComercialesID,
                v.Baja,
                estrellasEncendidas.TryGetValue(v.VendedoresID, out IReadOnlySet<int>? ids) ? ids : null))
            .ToList();
    }
}
