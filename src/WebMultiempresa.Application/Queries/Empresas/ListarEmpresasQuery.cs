using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Empresas;

public sealed class ListarEmpresasQuery
{
    private readonly IEmpresaRepository _repository;

    public ListarEmpresasQuery(IEmpresaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<EmpresaDto>> HandleAsync(CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Entities.Empresa> empresas =
            await _repository.ListarActivasAsync(cancellationToken);

        return empresas.Select(e => new EmpresaDto(
            EmpresaID: e.EmpresaID,
            Nombre: e.Nombre,
            KeyConexion: e.KeyConexion,
            Baja: e.Baja
        )).ToList();
    }
}
