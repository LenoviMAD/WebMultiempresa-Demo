using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Empresas;

public sealed class ObtenerEmpresaQuery
{
    private readonly IEmpresaRepository _repository;

    public ObtenerEmpresaQuery(IEmpresaRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmpresaDto?> HandleAsync(int empresaId, CancellationToken cancellationToken)
    {
        Domain.Entities.Empresa? e = await _repository.ObtenerPorIdAsync(empresaId, cancellationToken);

        if (e is null) return null;

        return new EmpresaDto(
            EmpresaID: e.EmpresaID,
            Nombre: e.Nombre,
            KeyConexion: e.KeyConexion,
            Baja: e.Baja);
    }
}
