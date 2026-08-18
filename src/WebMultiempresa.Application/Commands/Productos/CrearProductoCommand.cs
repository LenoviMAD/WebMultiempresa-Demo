namespace WebMultiempresa.Application.Commands.Productos;

public sealed class CrearProductoCommand
{
    public string Codigo { get; init; } = string.Empty;
    public string Nombre { get; init; } = string.Empty;
}
