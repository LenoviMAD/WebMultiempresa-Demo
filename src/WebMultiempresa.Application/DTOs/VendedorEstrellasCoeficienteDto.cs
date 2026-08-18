namespace WebMultiempresa.Application.DTOs;

public sealed record VendedorEstrellasCoeficienteDto(
    int VendedorEstrellasCoeficientesID,
    byte CantidadEstrellas,
    decimal CoeficienteComision);
