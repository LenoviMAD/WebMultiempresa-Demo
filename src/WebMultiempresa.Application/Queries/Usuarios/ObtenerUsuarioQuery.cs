using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Queries.Usuarios;

public sealed class ObtenerUsuarioQuery
{
    private readonly IUsuarioRepository _repository;

    public ObtenerUsuarioQuery(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioListadoDto?> HandleAsync(int usuariosId, CancellationToken cancellationToken)
    {
        Domain.Entities.Usuario? u = await _repository.ObtenerPorIdAsync(usuariosId, cancellationToken);

        if (u is null) return null;

        return new UsuarioListadoDto(
            UsuariosID: u.UsuariosID,
            EmpresaID: u.EmpresaID,
            NombreEmpresa: u.Empresa?.Nombre,
            Email: u.Email,
            Nombre: u.Nombre,
            Rol: u.Rol,
            Baja: u.Baja,
            FechaCreacion: u.FechaCreacion);
    }
}
