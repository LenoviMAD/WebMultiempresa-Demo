namespace WebMultiempresa.Application.DTOs;

public sealed record VendedorListaPreciosDto(
    int VendedorListasPreciosID,
    int ListasPreciosID,
    string NombreLista,
    bool EsDefault);
