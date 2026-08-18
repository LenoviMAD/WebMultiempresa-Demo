namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class QuitarClienteVendedorCommand
{
    public int ClientesID { get; init; }
    public int VendedoresID { get; init; }
}
