namespace WebMultiempresa.Domain.Entities;

public sealed class PlanCapacidad
{
    public int PlanCapacidadesID { get; private set; }
    public int PlanesID { get; private set; }
    public int TiposActoresID { get; private set; }
    public int MaxCapacidad { get; private set; }

    private PlanCapacidad() { }

    public static PlanCapacidad Crear(int planesId, int tiposActoresId, int maxCapacidad) =>
        new() { PlanesID = planesId, TiposActoresID = tiposActoresId, MaxCapacidad = maxCapacidad };

    public void ActualizarCapacidad(int maxCapacidad) => MaxCapacidad = maxCapacidad;
}
