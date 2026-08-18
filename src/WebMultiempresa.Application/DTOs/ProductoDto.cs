namespace WebMultiempresa.Application.DTOs;

public sealed record ProductoDto(
    int ProductosID,
    int EmpresaID,
    string Codigo,
    string Nombre,
    decimal? PrecioCosto,
    bool Baja,
    bool NuevoIngreso = false,
    int DiasDePrecioModificado = 9999,
    IReadOnlyList<ProductoPrecioDto>? Precios = null
);
