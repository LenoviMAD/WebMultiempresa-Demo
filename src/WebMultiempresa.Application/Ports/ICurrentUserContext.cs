namespace WebMultiempresa.Application.Ports;

public interface ICurrentUserContext
{
    int? UsuariosID { get; }
    string NombreUsuario { get; }
}
