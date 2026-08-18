using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;

namespace WebMultiempresa.Application.Commands.Auth;

public sealed class LoginVendedorCommandHandler
{
    private readonly IVendedorRepository _vendedorRepository;
    private readonly IAuthService        _authService;

    public LoginVendedorCommandHandler(
        IVendedorRepository vendedorRepository,
        IAuthService        authService)
    {
        _vendedorRepository = vendedorRepository;
        _authService        = authService;
    }

    public async Task<string> HandleAsync(LoginVendedorCommand command, CancellationToken cancellationToken)
    {
        Vendedor? vendedor = await _vendedorRepository.ObtenerPorCodigoAsync(
            command.Codigo, command.EmpresaID, cancellationToken);

        if (vendedor is null || vendedor.Baja)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        bool claveValida = string.Equals(vendedor.Clave, command.Clave, StringComparison.Ordinal);
        if (!claveValida)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        return _authService.GenerarTokenVendedor(
            vendedor.VendedoresID,
            vendedor.EmpresaID,
            vendedor.Nombre);
    }
}
