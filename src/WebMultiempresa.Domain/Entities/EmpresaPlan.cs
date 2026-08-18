namespace WebMultiempresa.Domain.Entities;

public sealed class EmpresaPlan
{
    public int EmpresaPlanesID { get; private set; }
    public int EmpresaID { get; private set; }
    public int PlanesID { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime? FechaVencimiento { get; private set; }
    public bool Baja { get; private set; }

    private EmpresaPlan() { }

    public static EmpresaPlan Crear(int empresaId, int planesId, DateTime? fechaVencimiento = null) =>
        new()
        {
            EmpresaID = empresaId,
            PlanesID = planesId,
            FechaInicio = DateTime.UtcNow,
            FechaVencimiento = fechaVencimiento,
            Baja = false
        };
}
