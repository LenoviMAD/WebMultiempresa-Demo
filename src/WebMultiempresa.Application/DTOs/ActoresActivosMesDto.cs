namespace WebMultiempresa.Application.DTOs;

public sealed record ActoresActivosMesDto(
    int EmpresaID,
    int TiposActoresID,
    DateTime InicioMes,
    DateTime FinMes,
    int CantidadActivos);
