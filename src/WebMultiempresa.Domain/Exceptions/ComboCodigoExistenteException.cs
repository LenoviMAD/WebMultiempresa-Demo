namespace WebMultiempresa.Domain.Exceptions;

public sealed class ComboCodigoExistenteException : Exception
{
    // Código que causó el conflicto, útil para el handler de la capa Application
    public string Codigo { get; }

    public ComboCodigoExistenteException(string codigo)
        : base($"Ya existe un combo con el código '{codigo}' en esta empresa.")
    {
        Codigo = codigo;
    }
}
