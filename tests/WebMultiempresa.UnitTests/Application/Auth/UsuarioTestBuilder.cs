using System.Reflection;
using WebMultiempresa.Domain.Entities;

namespace WebMultiempresa.UnitTests.Application.Auth;

/// <summary>
/// Builder de Usuario para tests — usa reflection para bypassear constructores privados.
/// </summary>
internal static class UsuarioTestBuilder
{
    internal static Usuario Crear(
        int usuariosId, int? empresaId, string email,
        string passwordHash, string nombre, byte rol, bool baja = false)
    {
        Usuario usuario = (Usuario)Activator.CreateInstance(typeof(Usuario), nonPublic: true)!;

        SetProp(usuario, nameof(Usuario.UsuariosID), usuariosId);
        SetProp(usuario, nameof(Usuario.EmpresaID), empresaId);
        SetProp(usuario, nameof(Usuario.Email), email);
        SetProp(usuario, nameof(Usuario.PasswordHash), passwordHash);
        SetProp(usuario, nameof(Usuario.Nombre), nombre);
        SetProp(usuario, nameof(Usuario.Rol), rol);
        SetProp(usuario, nameof(Usuario.Baja), baja);

        return usuario;
    }

    private static void SetProp(object obj, string name, object? value)
    {
        PropertyInfo prop = obj.GetType().GetProperty(name,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"Propiedad '{name}' no encontrada en {obj.GetType().Name}.");
        prop.SetValue(obj, value);
    }
}
