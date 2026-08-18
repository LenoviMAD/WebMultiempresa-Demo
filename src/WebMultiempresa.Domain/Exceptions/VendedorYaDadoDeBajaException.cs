namespace WebMultiempresa.Domain.Exceptions;

public sealed class VendedorYaDadoDeBajaException : Exception
{
    public VendedorYaDadoDeBajaException(int vendedoresId)
        : base($"El vendedor {vendedoresId} ya está dado de baja.") { }
}
