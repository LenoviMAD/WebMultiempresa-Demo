namespace WebMultiempresa.Application.DTOs;

public sealed record UsuarioListadoDto(
    int UsuariosID,
    int? EmpresaID,
    string? NombreEmpresa,
    string Email,
    string Nombre,
    byte Rol,
    bool Baja,
    DateTime FechaCreacion
);
