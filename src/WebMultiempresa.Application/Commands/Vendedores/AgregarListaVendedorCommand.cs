namespace WebMultiempresa.Application.Commands.Vendedores;

public sealed class AgregarListaVendedorCommand
{
    public int VendedoresID { get; init; }
    public int ListasPreciosID { get; init; }
    public bool EsDefault { get; init; }
}
