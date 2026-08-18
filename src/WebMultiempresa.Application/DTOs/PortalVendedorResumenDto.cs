namespace WebMultiempresa.Application.DTOs;

public sealed record PortalVendedorResumenDto(
    string NombreVendedor,
    decimal CoeficienteComision,
    bool CoeficientePositivo,
    IReadOnlyList<PortalEstrellaResumenDto> Estrellas);

public sealed record PortalEstrellaResumenDto(
    int      VendedorEstrellasDefinicionesID,
    byte     NumeroEstrella,
    string   Nombre,
    decimal? ObjetivoMensual,
    decimal  PromedioValor,
    bool     EstaEncendidaHoy);
