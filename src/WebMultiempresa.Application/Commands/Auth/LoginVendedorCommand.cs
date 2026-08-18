namespace WebMultiempresa.Application.Commands.Auth;

public sealed record LoginVendedorCommand(string Codigo, string Clave, int EmpresaID);
