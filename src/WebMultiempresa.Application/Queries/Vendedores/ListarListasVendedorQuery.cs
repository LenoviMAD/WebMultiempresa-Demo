using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Vendedores;

public sealed class ListarListasVendedorQuery
{
    private readonly IVendedorListaPreciosRepository _listasRepository;
    private readonly IListaPreciosRepository _listaPreciosRepository;

    public ListarListasVendedorQuery(
        IVendedorListaPreciosRepository listasRepository,
        IListaPreciosRepository listaPreciosRepository)
    {
        _listasRepository       = listasRepository;
        _listaPreciosRepository = listaPreciosRepository;
    }

    public async Task<IReadOnlyList<VendedorListaPreciosDto>> HandleAsync(
        int vendedoresId,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<VendedorListaPrecios> asignaciones =
            await _listasRepository.ListarActivasPorVendedorAsync(vendedoresId, cancellationToken);

        IReadOnlyList<ListaPrecios> todasLasListas =
            await _listaPreciosRepository.ListarActivasAsync(cancellationToken);

        Dictionary<int, string> nombresPorId = todasLasListas.ToDictionary(l => l.ListasPreciosID, l => l.Nombre);

        return asignaciones.Select(a => new VendedorListaPreciosDto(
            a.VendedorListasPreciosID,
            a.ListasPreciosID,
            nombresPorId.GetValueOrDefault(a.ListasPreciosID, "—"),
            a.EsDefault)).ToList();
    }
}
