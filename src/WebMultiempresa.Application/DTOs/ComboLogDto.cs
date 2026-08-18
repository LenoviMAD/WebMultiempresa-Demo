namespace WebMultiempresa.Application.DTOs;

public sealed record ComboLogDto(
    int ComboLogsID,
    string NombreUsuario,
    string PC,
    string Comentario,
    DateTime Fecha
);
