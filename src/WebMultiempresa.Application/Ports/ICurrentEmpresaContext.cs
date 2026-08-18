namespace WebMultiempresa.Application.Ports;

/// <summary>
/// Puerto que expone el contexto de empresa activa para la solicitud en curso.
/// Implementado en Infrastructure/Blazor a partir del token JWT.
/// </summary>
public interface ICurrentEmpresaContext
{
    int? EmpresaID { get; }
}
