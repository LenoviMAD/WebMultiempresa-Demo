namespace WebMultiempresa.Application.Commands.Empresas;

public sealed class CrearEmpresaCommand
{
    public string Nombre { get; init; } = string.Empty;
    public string KeyConexion { get; init; } = string.Empty;
}
