namespace WebMultiempresa.Domain.Exceptions;

public sealed class VendedorCodigoExistenteException : Exception
{
    public VendedorCodigoExistenteException(string codigo)
        : base($"Ya existe un vendedor con el código '{codigo}' en esta empresa.") { }
}
