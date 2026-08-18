using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.ListasPrecios;

public sealed class ListarListasPreciosQuery
{
    private readonly IListaPreciosRepository _repository;

    public ListarListasPreciosQuery(IListaPreciosRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListaPreciosDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<ListaPrecios> listas = await _repository.ListarActivasAsync(cancellationToken);
        return listas.Select(l => new ListaPreciosDto(l.ListasPreciosID, l.Nombre)).ToList();
    }
}
