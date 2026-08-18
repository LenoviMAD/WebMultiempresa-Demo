namespace WebMultiempresa.Domain.Exceptions;

public sealed class ComboSinTipoClienteException : Exception
{
    public ComboSinTipoClienteException()
        : base("El combo debe aplicar al menos a un tipo de cliente (numérico o alfanumérico).") { }
}
