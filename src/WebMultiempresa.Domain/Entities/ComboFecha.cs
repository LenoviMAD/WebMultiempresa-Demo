namespace WebMultiempresa.Domain.Entities;

public sealed class ComboFecha
{
    public int ComboFechasID { get; private set; }
    public int CombosID { get; private set; }
    public DateTime FechaInicial { get; private set; }
    public DateTime FechaFinal { get; private set; }

    private ComboFecha() { }

    public static ComboFecha Crear(int combosId, DateTime fechaInicial, DateTime fechaFinal) =>
        new() { CombosID = combosId, FechaInicial = fechaInicial, FechaFinal = fechaFinal };
}
