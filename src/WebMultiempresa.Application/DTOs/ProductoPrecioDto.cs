namespace WebMultiempresa.Application.DTOs;

public sealed record ProductoPrecioDto(
    int ListasPreciosID,
    string NombreLista,
    decimal PrecioFinal
);
