using WebMultiempresa.Application.Ports;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Infrastructure.Services;

public sealed class BlazorCurrentUserContext : ICurrentUserContext
{
    private readonly UsuarioContexto _usuarioContexto;

    public BlazorCurrentUserContext(UsuarioContexto usuarioContexto)
    {
        _usuarioContexto = usuarioContexto;
    }

    public int? UsuariosID => _usuarioContexto.UsuariosID;
    public string NombreUsuario => _usuarioContexto.NombreUsuario;
}
