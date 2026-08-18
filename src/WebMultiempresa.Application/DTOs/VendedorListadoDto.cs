namespace WebMultiempresa.Application.DTOs;

public sealed record VendedorListadoDto(
    int VendedoresID,
    string Codigo,
    string Nombre,
    string? WhatsApp,
    int? CategoriasComercialesID,
    bool Baja,
    IReadOnlySet<int>? EstrellaDefinicionesEncendidas = null);
