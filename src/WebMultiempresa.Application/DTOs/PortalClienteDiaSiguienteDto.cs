namespace WebMultiempresa.Application.DTOs;

public sealed record PortalClienteDiaSiguienteDto(
    int     ClientesID,
    string  Codigo,
    string  Nombre,
    string? Telefono,
    string? Direccion);
