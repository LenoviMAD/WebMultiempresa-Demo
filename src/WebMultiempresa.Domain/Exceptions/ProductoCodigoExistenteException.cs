namespace WebMultiempresa.Domain.Exceptions;

public sealed class ProductoCodigoExistenteException : Exception
{
    public string Codigo { get; }

    public ProductoCodigoExistenteException(string codigo)
        : base($"Ya existe un producto con el código '{codigo}' en esta empresa.")
    {
        Codigo = codigo;
    }
}
