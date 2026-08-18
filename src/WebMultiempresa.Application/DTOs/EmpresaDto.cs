namespace WebMultiempresa.Application.DTOs;

public sealed record EmpresaDto(
    int EmpresaID,
    string Nombre,
    string KeyConexion,
    bool Baja
);
