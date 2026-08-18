namespace WebMultiempresa.Domain.Entities;

public sealed class EmpresaAplicacion
{
    public int EmpresaAplicacionesID { get; private set; }
    public int EmpresaID { get; private set; }
    public int AplicacionesID { get; private set; }
    public DateTime FechaActivacion { get; private set; }
    public DateTime? FechaVencimiento { get; private set; }
    public bool Baja { get; private set; }

    private EmpresaAplicacion() { }

    public static EmpresaAplicacion Crear(int empresaId, int aplicacionesId, DateTime? fechaVencimiento = null) =>
        new()
        {
            EmpresaID = empresaId,
            AplicacionesID = aplicacionesId,
            FechaActivacion = DateTime.UtcNow,
            FechaVencimiento = fechaVencimiento,
            Baja = false
        };
}
