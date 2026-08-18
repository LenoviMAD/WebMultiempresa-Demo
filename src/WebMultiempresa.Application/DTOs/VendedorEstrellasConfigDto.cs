namespace WebMultiempresa.Application.DTOs;

/// <summary>Vista unificada de una estrella: definición + coeficiente juntos.</summary>
public sealed record VendedorEstrellasConfigDto(
    int DefinicionId,
    int CoeficienteId,
    byte NumeroEstrella,
    string Nombre,
    decimal? ObjetivoMensual,
    decimal CoeficienteComision);
