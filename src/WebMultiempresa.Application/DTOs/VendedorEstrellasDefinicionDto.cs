namespace WebMultiempresa.Application.DTOs;

public sealed record VendedorEstrellasDefinicionDto(
    int VendedorEstrellasDefinicionesID,
    byte NumeroEstrella,
    string Nombre,
    decimal? ObjetivoMensual);
