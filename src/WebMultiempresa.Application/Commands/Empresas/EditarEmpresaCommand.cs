namespace WebMultiempresa.Application.Commands.Empresas;

public sealed class EditarEmpresaCommand
{
    public int EmpresaID { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string KeyConexion { get; init; } = string.Empty;
}
