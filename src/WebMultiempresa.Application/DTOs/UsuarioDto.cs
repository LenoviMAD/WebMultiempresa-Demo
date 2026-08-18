namespace WebMultiempresa.Application.DTOs;

/// <summary>
/// DTO de solo lectura que representa los datos públicos de un usuario autenticado.
/// Nunca se expone la entidad de dominio directamente.
/// </summary>
public sealed record UsuarioDto(
    int UsuariosID,
    int? EmpresaID,
    string Email,
    string Nombre,
    byte Rol
);
