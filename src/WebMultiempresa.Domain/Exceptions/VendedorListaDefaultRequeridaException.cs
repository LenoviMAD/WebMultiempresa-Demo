namespace WebMultiempresa.Domain.Exceptions;

public sealed class VendedorListaDefaultRequeridaException : Exception
{
    public VendedorListaDefaultRequeridaException()
        : base("No se puede quitar la única lista de precios default del vendedor.") { }
}
