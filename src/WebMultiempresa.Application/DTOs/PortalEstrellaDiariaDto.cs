namespace WebMultiempresa.Application.DTOs;

public sealed record PortalEstrellaDiariaDto(
    DateTime Fecha,
    decimal  Valor,
    bool     EstaEncendida);
