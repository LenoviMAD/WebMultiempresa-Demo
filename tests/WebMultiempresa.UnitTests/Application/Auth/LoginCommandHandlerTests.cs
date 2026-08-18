using Moq;
using WebMultiempresa.Application.Commands.Auth;
using WebMultiempresa.Application.DTOs;
using WebMultiempresa.Application.Ports;
using WebMultiempresa.Domain.Entities;
using WebMultiempresa.Domain.Interfaces;
using Xunit;

namespace WebMultiempresa.UnitTests.Application.Auth;

public sealed class LoginCommandHandlerTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new();
    private readonly Mock<IAuthService> _authServiceMock = new();
    private readonly Mock<IPasswordHasher> _passwordHasherMock = new();

    private LoginCommandHandler CrearHandler() =>
        new(_usuarioRepositoryMock.Object, _authServiceMock.Object, _passwordHasherMock.Object);

    [Fact]
    public async Task HandleAsync_CredencialesValidas_RetornaToken()
    {
        // Arrange
        Usuario usuario = UsuarioTestBuilder.Crear(
            usuariosId: 1,
            empresaId: 2,
            email: "admin@empresa.com",
            passwordHash: "hash-irrelevante",
            nombre: "Admin Test",
            rol: 2
        );

        _usuarioRepositoryMock
            .Setup(r => r.ObtenerPorEmailAsync("admin@empresa.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuario);

        _passwordHasherMock
            .Setup(p => p.Verificar("Clave123!", "hash-irrelevante"))
            .Returns(true);

        _authServiceMock
            .Setup(a => a.GenerarToken(1, 2, 2))
            .Returns("jwt-token-fake");

        LoginCommandHandler handler = CrearHandler();
        LoginCommand command = new("admin@empresa.com", "Clave123!");

        // Act
        LoginResultDto resultado = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        Assert.Equal("jwt-token-fake", resultado.Token);
        Assert.Equal(1, resultado.Usuario.UsuariosID);
        Assert.Equal(2, resultado.Usuario.EmpresaID);
        Assert.Equal("Admin Test", resultado.Usuario.Nombre);
        _authServiceMock.Verify(a => a.GenerarToken(1, 2, 2), Times.Once());
    }

    [Fact]
    public async Task HandleAsync_EmailInexistente_LanzaUnauthorizedAccessException()
    {
        // Arrange
        _usuarioRepositoryMock
            .Setup(r => r.ObtenerPorEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Usuario?)null);

        LoginCommandHandler handler = CrearHandler();
        LoginCommand command = new("noexiste@empresa.com", "cualquier");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => handler.HandleAsync(command, CancellationToken.None));
    }

    [Fact]
    public async Task HandleAsync_PasswordIncorrecta_LanzaUnauthorizedAccessException()
    {
        // Arrange
        Usuario usuario = UsuarioTestBuilder.Crear(
            usuariosId: 1,
            empresaId: 2,
            email: "admin@empresa.com",
            passwordHash: "hash-correcto",
            nombre: "Admin Test",
            rol: 2
        );

        _usuarioRepositoryMock
            .Setup(r => r.ObtenerPorEmailAsync("admin@empresa.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuario);

        _passwordHasherMock
            .Setup(p => p.Verificar("ClaveIncorrecta!", "hash-correcto"))
            .Returns(false);

        LoginCommandHandler handler = CrearHandler();
        LoginCommand command = new("admin@empresa.com", "ClaveIncorrecta!");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => handler.HandleAsync(command, CancellationToken.None));
    }

    [Fact]
    public async Task HandleAsync_UsuarioDadoDeBaja_LanzaUnauthorizedAccessException()
    {
        // Arrange
        Usuario usuario = UsuarioTestBuilder.Crear(
            usuariosId: 1, empresaId: 2,
            email: "baja@empresa.com",
            passwordHash: "hash-cualquiera",
            nombre: "Usuario Baja",
            rol: 2, baja: true
        );

        _usuarioRepositoryMock
            .Setup(r => r.ObtenerPorEmailAsync("baja@empresa.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(usuario);

        LoginCommandHandler handler = CrearHandler();
        LoginCommand command = new("baja@empresa.com", "Clave123!");

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => handler.HandleAsync(command, CancellationToken.None));
    }
}
