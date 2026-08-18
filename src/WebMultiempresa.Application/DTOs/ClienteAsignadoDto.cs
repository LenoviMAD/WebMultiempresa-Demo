namespace WebMultiempresa.Application.DTOs;

public sealed record ClienteAsignadoDto(
    int ClienteVendedoresID,
    int ClientesID,
    string Codigo,
    string Nombre);
