// src/WebMultiempresa.Blazor/Auth/BlazorAuthStateProvider.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using WebMultiempresa.Infrastructure.Persistence;

namespace WebMultiempresa.Blazor.Auth;

public sealed class BlazorAuthStateProvider : AuthenticationStateProvider
{
    private readonly IJSRuntime _jsRuntime;
    private readonly EmpresaContexto _empresaContexto;
    private readonly UsuarioContexto _usuarioContexto;
    private readonly VendedorContexto _vendedorContexto;

    public BlazorAuthStateProvider(
        IJSRuntime jsRuntime,
        EmpresaContexto empresaContexto,
        UsuarioContexto usuarioContexto,
        VendedorContexto vendedorContexto)
    {
        _jsRuntime        = jsRuntime;
        _empresaContexto  = empresaContexto;
        _usuarioContexto  = usuarioContexto;
        _vendedorContexto = vendedorContexto;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await ObtenerTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
            return EstadoAnonimo();

        ClaimsPrincipal principal = ParsearToken(token);
        SincronizarContexto(principal);

        return new AuthenticationState(principal);
    }

    public async Task LoginAsync(string token)
    {
        await GuardarTokenAsync(token);
        ClaimsPrincipal principal = ParsearToken(token);
        SincronizarContexto(principal);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task LogoutAsync()
    {
        await EliminarTokenAsync();
        _empresaContexto.EmpresaID     = null;
        _usuarioContexto.UsuariosID    = null;
        _usuarioContexto.NombreUsuario = string.Empty;
        _vendedorContexto.VendedoresID   = null;
        _vendedorContexto.NombreVendedor = string.Empty;
        NotifyAuthenticationStateChanged(Task.FromResult(EstadoAnonimo()));
    }

    private void SincronizarContexto(ClaimsPrincipal principal)
    {
        string? empresaIdStr = principal.FindFirst("empresa_id")?.Value;
        _empresaContexto.EmpresaID = int.TryParse(empresaIdStr, out int empresaId) ? empresaId : null;

        string? usuarioIdStr = principal.FindFirst("usuario_id")?.Value;
        _usuarioContexto.UsuariosID = int.TryParse(usuarioIdStr, out int usuarioId) ? usuarioId : null;
        _usuarioContexto.NombreUsuario = principal.FindFirst("name")?.Value
            ?? principal.FindFirst("email")?.Value
            ?? string.Empty;

        string? vendedorIdStr = principal.FindFirst("vendedor_id")?.Value;
        _vendedorContexto.VendedoresID    = int.TryParse(vendedorIdStr, out int vendedorId) ? vendedorId : null;
        _vendedorContexto.NombreVendedor  = principal.FindFirst("name")?.Value ?? string.Empty;
    }

    private async Task<string?> ObtenerTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "auth_token");
        }
        catch
        {
            // Durante el prerender no hay JS disponible
            return null;
        }
    }

    private async Task GuardarTokenAsync(string token) =>
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "auth_token", token);

    private async Task EliminarTokenAsync() =>
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "auth_token");

    private static ClaimsPrincipal ParsearToken(string token)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwt = handler.ReadJwtToken(token);
        ClaimsIdentity identity = new(jwt.Claims, "jwt");
        return new ClaimsPrincipal(identity);
    }

    private static AuthenticationState EstadoAnonimo() =>
        new(new ClaimsPrincipal(new ClaimsIdentity()));
}
