namespace WebMultiempresa.Application.Commands.Auth;

/// <summary>
/// Comando que transporta las credenciales del usuario para el caso de uso de login.
/// </summary>
public sealed record LoginCommand(string Email, string Password);
