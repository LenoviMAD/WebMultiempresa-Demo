namespace WebMultiempresa.Domain.Exceptions;

public sealed class VendedorYaActivoException : Exception
{
    public VendedorYaActivoException(int vendedoresId)
        : base($"El vendedor {vendedoresId} ya está activo.") { }
}
