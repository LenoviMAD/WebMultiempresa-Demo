namespace WebMultiempresa.Application.DTOs;

public sealed record VendedorDto(
    int VendedoresID,
    string Codigo,
    string Nombre,
    string? WhatsApp,
    int? CategoriasComercialesID,
    bool VerTodasLasCategorias,
    string? Clave,
    int DiaInicioRuta,
    bool Baja);
