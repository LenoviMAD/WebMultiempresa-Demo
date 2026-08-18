using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebMultiempresa.Application.Ports;

namespace WebMultiempresa.Infrastructure.Services;

/// <summary>
/// Adaptador que implementa el puerto IAuthService mediante JWT.
/// Genera tokens firmados con HMAC-SHA256 e incluye los claims del usuario.
/// </summary>
public sealed class JwtAuthService : IAuthService
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiresHours;

    public JwtAuthService(IConfiguration configuration)
    {
        _secretKey    = configuration["Jwt:SecretKey"]    ?? throw new InvalidOperationException("Jwt:SecretKey no configurado.");
        _issuer       = configuration["Jwt:Issuer"]       ?? "WebMultiempresa";
        _audience     = configuration["Jwt:Audience"]     ?? "WebMultiempresaApp";
        // Validar que ExpiresHours sea un entero válido antes de asignarlo
        string expiresHoursValue = configuration["Jwt:ExpiresHours"] ?? "8";
        if (!int.TryParse(expiresHoursValue, out int expiresHours))
            throw new InvalidOperationException($"Jwt:ExpiresHours tiene un valor inválido: '{expiresHoursValue}'. Debe ser un entero.");
        _expiresHours = expiresHours;
    }

    /// <summary>
    /// Genera un token JWT firmado con los claims del usuario.
    /// El claim empresa_id se omite cuando el usuario es SuperAdmin (empresaId null).
    /// </summary>
    public string GenerarToken(int usuarioId, int? empresaId, byte rol)
    {
        List<Claim> claims = new()
        {
            new Claim("usuario_id", usuarioId.ToString()),
            new Claim("rol", rol.ToString())
        };

        if (empresaId.HasValue)
            claims.Add(new Claim("empresa_id", empresaId.Value.ToString()));

        SymmetricSecurityKey key   = new(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials   creds = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer:             _issuer,
            audience:           _audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(_expiresHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerarTokenVendedor(int vendedoresId, int empresaId, string nombreVendedor)
    {
        List<Claim> claims =
        [
            new Claim("vendedor_id", vendedoresId.ToString()),
            new Claim("empresa_id",  empresaId.ToString()),
            new Claim("name",        nombreVendedor),
            new Claim("tipo",        "vendedor")
        ];

        SymmetricSecurityKey key   = new(Encoding.UTF8.GetBytes(_secretKey));
        SigningCredentials   creds = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            issuer:             _issuer,
            audience:           _audience,
            claims:             claims,
            expires:            DateTime.UtcNow.AddHours(_expiresHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Extrae el EmpresaId del token JWT sin validar firma ni expiración.
    /// Retorna null si el token es nulo, vacío, malformado, o si el claim no está presente
    /// o el valor no es un entero válido.
    /// </summary>
    public int? ObtenerEmpresaIdDelToken(string token)
    {
        // Guard: rechazar tokens nulos o vacíos antes de intentar parsear
        if (string.IsNullOrWhiteSpace(token))
            return null;

        try
        {
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken        jwt     = handler.ReadJwtToken(token);

            string? empresaIdStr = jwt.Claims.FirstOrDefault(c => c.Type == "empresa_id")?.Value;

            if (empresaIdStr is null)
                return null;

            return int.TryParse(empresaIdStr, out int id) ? id : null;
        }
        catch (ArgumentException)
        {
            // Token malformado: no es un JWT válido
            return null;
        }
    }
}
