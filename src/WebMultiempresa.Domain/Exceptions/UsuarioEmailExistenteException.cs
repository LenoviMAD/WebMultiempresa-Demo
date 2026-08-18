namespace WebMultiempresa.Domain.Exceptions;

public sealed class UsuarioEmailExistenteException : Exception
{
    public string Email { get; }

    public UsuarioEmailExistenteException(string email)
        : base($"Ya existe un usuario con el email '{email}'.")
    {
        Email = email;
    }
}
