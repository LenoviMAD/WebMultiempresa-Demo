namespace WebMultiempresa.Domain.Exceptions;

public sealed class ComboSinListaPrecioException : Exception
{
    public ComboSinListaPrecioException()
        : base("El combo debe tener al menos una lista de precios asignada.") { }
}
