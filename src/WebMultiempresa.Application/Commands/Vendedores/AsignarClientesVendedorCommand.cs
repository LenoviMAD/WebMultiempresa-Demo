namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class AsignarClientesVendedorCommand
{
    public int VendedoresID { get; init; }
    public IReadOnlyList<int> ClientesIDs { get; init; } = [];
}
