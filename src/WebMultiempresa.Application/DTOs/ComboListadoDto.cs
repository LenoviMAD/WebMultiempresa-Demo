namespace WebMultiempresa.Application.DTOs;

public sealed record ComboListadoDto(
    int CombosID,
    string Nombre,
    string Codigo,
    string NombreMarcaProducto,
    DateTime FechaInicio,
    DateTime FechaVigencia,
    bool Vigente,
    bool Baja
);
