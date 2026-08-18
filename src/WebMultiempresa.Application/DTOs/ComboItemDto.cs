namespace WebMultiempresa.Application.DTOs;

public sealed record ComboItemDto(
    int ComboItemsID,
    int ProductosID,
    string NombreProducto,
    string CodigoProducto,
    decimal Cantidad,
    decimal Precio,
    byte Tipo,
    decimal Descuento1,
    decimal Descuento2,
    int NroGrupoDinamico
);
