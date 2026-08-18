using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.CategoriasComerciales;

public sealed class ListarCategoriasComercalesQuery
{
    private readonly ICategoriaComercialRepository _repository;

    public ListarCategoriasComercalesQuery(ICategoriaComercialRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CategoriaComercialDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<CategoriaComercial> categorias = await _repository.ListarActivasAsync(cancellationToken);
        return categorias.Select(c => new CategoriaComercialDto(c.CategoriasComercialesID, c.Nombre)).ToList();
    }
}
