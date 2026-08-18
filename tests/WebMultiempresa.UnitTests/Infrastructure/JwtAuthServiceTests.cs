using Microsoft.Extensions.Configuration;
using WebMultiempresa.Infrastructure.Services;
using Xunit;

namespace WebMultiempresa.UnitTests.Infrastructure;

/// <summary>
/// Tests unitarios para JwtAuthService.
/// Verifican la generación y lectura de tokens JWT con y sin empresa asociada.
/// </summary>
public sealed class JwtAuthServiceTests
{
    /// <summary>Crea una instancia de JwtAuthService con configuración en memoria para tests.</summary>
    private static JwtAuthService CrearService()
    {
        Dictionary<string, string?> config = new()
        {
            ["Jwt:SecretKey"]    = "clave-secreta-de-test-minimo-32-chars!!",
            ["Jwt:Issuer"]       = "WebMultiempresa",
            ["Jwt:Audience"]     = "WebMultiempresaApp",
            ["Jwt:ExpiresHours"] = "8"
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();

        return new JwtAuthService(configuration);
    }

    [Fact]
    public void GenerarToken_UsuarioConEmpresa_TokenContieneClaims()
    {
        // Arrange
        JwtAuthService service = CrearService();

        // Act
        string token = service.GenerarToken(usuarioId: 1, empresaId: 3, rol: 2);

        // Assert
        Assert.NotEmpty(token);

        int? empresaId = service.ObtenerEmpresaIdDelToken(token);
        Assert.Equal(3, empresaId);
    }

    [Fact]
    public void GenerarToken_SuperAdmin_EmpresaIdEsNull()
    {
        // Arrange
        JwtAuthService service = CrearService();

        // Act
        string token = service.GenerarToken(usuarioId: 1, empresaId: null, rol: 1);

        // Assert
        int? empresaId = service.ObtenerEmpresaIdDelToken(token);
        Assert.Null(empresaId);
    }

    [Fact]
    public void ObtenerEmpresaId_TokenMalformado_RetornaNull()
    {
        // Arrange
        JwtAuthService service = CrearService();

        // Act
        int? resultado = service.ObtenerEmpresaIdDelToken("esto-no-es-un-jwt");

        // Assert
        Assert.Null(resultado);
    }
}
